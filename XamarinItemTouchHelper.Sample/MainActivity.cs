using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget.Helper;
using Android.Support.V7.Widget;

namespace XamarinItemTouchHelper.Sample
{
    [Activity (Label = "XamarinItemTouchHelper.Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, RecyclerListAdapter.IOnStartDragListener
    {
        private ItemTouchHelper mItemTouchHelper;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            RecyclerListAdapter adapter = new RecyclerListAdapter(this);

            RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapter);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));

            ItemTouchHelper.Callback callback = new SimpleItemTouchHelperCallback(adapter);
            mItemTouchHelper = new ItemTouchHelper(callback);
            mItemTouchHelper.AttachToRecyclerView(recyclerView);
        }

        public void OnStartDrag (RecyclerView.ViewHolder viewHolder)
        {
            mItemTouchHelper.StartDrag(viewHolder);
        }
    }
}


