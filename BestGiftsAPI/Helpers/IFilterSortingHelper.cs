using BestGiftsAPI.Entities;
using BestGiftsAPI.Other_Models;
using System;
using System.Linq.Expressions;

namespace BestGiftsAPI.Helpers
{
    public interface IFilterSortingHelper
    {
        Expression<Func<GiftIdea, int>> PrepareSorting(SortingModel sort, ref bool descending);
    }
}
