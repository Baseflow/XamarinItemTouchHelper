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
    public interface IItemTouchHelperViewHolder
    {
        /// <summary>
        /// Called when the ItemTouchHelper first registers an item as being moved or swiped.
        /// Implementations should update the item view to indicate it's active state.
        /// </summary>
        void OnItemSelected();

        /// <summary>
        /// Called when the ItemTouchHelper has completed the move or swipe, and the active item
        /// state should be cleared.
        /// </summary>
        void OnItemClear();
    }
}