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

namespace XamarinItemTouchHelper
{
    public interface IItemTouchHelperAdapter
    {
        /// <summary>
        /// Called when an item has been dragged far enough to trigger a move. This is called every time
        /// an item is shifted, and <strong>not</strong> at the end of a "drop" event.<br/>
        /// <br/>
        /// Implementations should call {@link RecyclerView.Adapter#notifyItemMoved(int, int)} after
        /// adjusting the underlying data to reflect this move.
        /// </summary>
        /// <param name="fromPosition">The start position of the moved item.</param>
        /// <param name="toPosition">Then resolved position of the moved item.</param>
        void OnItemMove(int fromPosition, int toPosition);

        /// <summary>
        /// Called when an item has been dismissed by a swipe.<br/>
        /// <br/>
        /// Implementations should call {@link RecyclerView.Adapter#notifyItemRemoved(int)} after
        /// adjusting the underlying data to reflect this removal.
        /// </summary>
        /// <param name="position">The position of the item dismissed.</param>
        void OnItemDismiss(int position);
    }
}