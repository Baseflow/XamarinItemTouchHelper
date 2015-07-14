using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Support.V7.Widget;
using Android.Graphics;

namespace XamarinItemTouchHelper
{
    public class SimpleItemTouchHelperCallback : ItemTouchHelper.Callback
    {

        public static float AlphaFull = 1.0f;

        private IItemTouchHelperAdapter mAdapter;

        public SimpleItemTouchHelperCallback(IItemTouchHelperAdapter adapter)
        {
            mAdapter = adapter;
        }

        public override bool IsLongPressDragEnabled {
            get {
                return true;
            }
        }

        public override bool IsItemViewSwipeEnabled {
            get {
                return true;
            }
        }

        public override int GetMovementFlags (RecyclerView p0, RecyclerView.ViewHolder p1)
        {
            // Enable drag and swipe in both directions
            int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
            int swipeFlags = ItemTouchHelper.Start | ItemTouchHelper.End;
            return MakeMovementFlags(dragFlags, swipeFlags);
        }

        public override bool OnMove (RecyclerView recyclerView, RecyclerView.ViewHolder source, RecyclerView.ViewHolder target)
        {
            if (source.ItemViewType != target.ItemViewType) {
                return false;
            }

            // Notify the adapter of the move
            mAdapter.OnItemMove(source.AdapterPosition, target.AdapterPosition);
            return true;
        }

        public override void OnSwiped (Android.Support.V7.Widget.RecyclerView.ViewHolder viewHolder, int i)
        {
            // Notify the adapter of the dismissal
            mAdapter.OnItemDismiss(viewHolder.AdapterPosition);
        }

        public override void OnChildDraw (Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);

            // Fade out the view as it is swiped out of the parent's bounds
            if (actionState == ItemTouchHelper.ActionStateSwipe) {
                View itemView = viewHolder.ItemView;
                float alpha = AlphaFull - Math.Abs(dX) / (float) itemView.Width;
                itemView.Alpha = alpha;
            }
        }

        public override void OnSelectedChanged (RecyclerView.ViewHolder viewHolder, int actionState)
        {
            if (actionState != ItemTouchHelper.ActionStateIdle) {
                // Let the view holder know that this item is being moved or dragged
                IItemTouchHelperViewHolder itemViewHolder = (IItemTouchHelperViewHolder) viewHolder;
                itemViewHolder.OnItemSelected();
            }

            base.OnSelectedChanged(viewHolder, actionState);
        }

        public override void ClearView (RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            base.ClearView(recyclerView, viewHolder);

            viewHolder.ItemView.Alpha = AlphaFull;

            // Tell the view holder it's time to restore the idle state
            IItemTouchHelperViewHolder itemViewHolder = (IItemTouchHelperViewHolder) viewHolder;
            itemViewHolder.OnItemClear();
        }
    }
}