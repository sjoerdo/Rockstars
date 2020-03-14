using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;

namespace Rockstars.Adapters
{
    /// <summary>
    /// RecylerViewHolder
    /// </summary>
    public class RecylerViewHolder : RecyclerView.ViewHolder
    {
        public TextView NameTxt;
        public RecylerViewHolder(View itemView, Action<int> listener)
            : base(itemView)
        {
            NameTxt = itemView.FindViewById<TextView>(Resource.Id.nameTxt);
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }
}