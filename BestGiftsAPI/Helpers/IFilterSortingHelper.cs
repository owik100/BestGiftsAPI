using BestGiftsAPI.Entities;
using BestGiftsAPI.FilterSorting_models;
using BestGiftsAPI.FilterSortingmodels;
using System;
using System.Linq.Expressions;

namespace BestGiftsAPI.Helpers
{
    public interface IFilterSortingHelper
    {
        Expression<Func<GiftIdea, int>> PrepareSorting(SortingModel sort, ref bool descending);
        Expression<Func<GiftIdea, bool>> PrepareFilter(FilterModel filterModel);
    }
}
