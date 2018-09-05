using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platform;
using TestToDo1.Core.IRepository;
using TestToDo1.Core.Models;

namespace TestToDo1.Droid.Helper
{
    class Swipe2DismissTouchHelperCallback : ItemTouchHelper.SimpleCallback
    {
        private readonly Context _context;

        public Swipe2DismissTouchHelperCallback(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public Swipe2DismissTouchHelperCallback(Context context) : base(0, ItemTouchHelper.Left)
        {
            _context = context;
        }

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
        {
            return false;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            var holder = (MvxRecyclerViewHolder)viewHolder;
            var item = (Item)holder.DataContext;
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(_context);
            alertDialog.SetTitle("Delete task");
            alertDialog.SetMessage("do you really want to delete this task");
            //without create new ViewModel
            alertDialog.SetPositiveButton("Yes", delegate {
                Mvx.Resolve<IItemRepository>().Delete(item.Id);
                alertDialog.Dispose();
            });
            alertDialog.SetNegativeButton("No", delegate { alertDialog.Dispose(); });
            alertDialog.Show();

        }
    }
}