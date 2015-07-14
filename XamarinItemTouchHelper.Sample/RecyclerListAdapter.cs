using System;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Linq;
using Java.Util;
using System.Collections.ObjectModel;
using Android.Support.V4.View;

namespace XamarinItemTouchHelper.Sample
{
    public class RecyclerListAdapter : RecyclerView.Adapter, IItemTouchHelperAdapter
    {
        /// <summary>
        /// Listener for manual initiation of a drag.
        /// </summary>
        public interface IOnStartDragListener {
            /// <summary>
            /// Called when a view is requesting a start of a drag.
            /// </summary>
            /// <param name="viewHolder">The holder of the view to drag.</param>
            void OnStartDrag(RecyclerView.ViewHolder viewHolder);
        }

        private static string[] STRINGS = new String[]{
            "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten"
        };

        private ObservableCollection<string> mItems = new ObservableCollection<string>();

        private IOnStartDragListener mDragStartListener;

        public RecyclerListAdapter(IOnStartDragListener dragStartListener) {
            mDragStartListener = dragStartListener;

            foreach (var item in STRINGS) {
                mItems.Add(item);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_main, parent, false);
            ItemViewHolder itemViewHolder = new ItemViewHolder(view);
            return itemViewHolder;
        }

        public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
        {
            var itemHolder = (ItemViewHolder)holder;

            itemHolder.textView.Text = mItems.ElementAt(position);
            itemHolder.handleView.SetOnTouchListener (new TouchListenerHelper(itemHolder, mDragStartListener));
        }

        public void OnItemMove (int fromPosition, int toPosition)
        {
            mItems.Move(fromPosition, toPosition);
            NotifyItemMoved(fromPosition, toPosition);
        }
        public void OnItemDismiss (int position)
        {
            mItems.Remove(mItems.ElementAt(position));
            NotifyItemRemoved(position);
        }

        public override int ItemCount {
            get {
                return mItems.Count;
            }
        }

        /// <summary>
        /// Simple example of a view holder that implements ItemTouchHelperViewHolder and has a
        /// "handle" view that initiates a drag event when touched.
        /// </summary>
        public class ItemViewHolder : RecyclerView.ViewHolder, IItemTouchHelperViewHolder {

            public TextView textView;
            public ImageView handleView;
            public View _itemView;

            public ItemViewHolder (View itemView) : base (itemView)
            {
                _itemView = itemView;
                textView = (TextView) itemView.FindViewById(Resource.Id.textView1);
                handleView = (ImageView) itemView.FindViewById(Resource.Id.imageView1);
            }

            public void OnItemSelected ()
            {
                _itemView.SetBackgroundColor(Color.LightGray);
            }

            public void OnItemClear ()
            {
                _itemView.SetBackgroundColor(Color.White);
            }
        }
    }
}

