using System;
using Android.Views;
using Android.Support.V4.View;

namespace XamarinItemTouchHelper.Sample
{
    public class TouchListenerHelper : Java.Lang.Object, View.IOnTouchListener
    {
        private RecyclerListAdapter.ItemViewHolder _itemHolder;
        private RecyclerListAdapter.IOnStartDragListener _mDragStartListener;

        public TouchListenerHelper(RecyclerListAdapter.ItemViewHolder holder, RecyclerListAdapter.IOnStartDragListener mDragStartListener)
        {
            _itemHolder = holder;
            _mDragStartListener = mDragStartListener;
        }

        public bool OnTouch (View v, MotionEvent e)
        {

            if (MotionEventCompat.GetActionMasked(e) == MotionEventCompat.ActionPointerDown) {
                _mDragStartListener.OnStartDrag(_itemHolder);
            }
            return false;
        }
    }
}

