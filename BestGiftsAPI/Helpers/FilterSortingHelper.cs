using BestGiftsAPI.Entities;
using BestGiftsAPI.FilterSorting_models;
using BestGiftsAPI.FilterSortingmodels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BestGiftsAPI.Helpers
{
    public class FilterSortingHelper: IFilterSortingHelper
    {
        public Expression<Func<GiftIdea, int>> PrepareSorting(SortingModel sort, ref bool descending)
        {
            Expression<Func<GiftIdea, int>> result = null;
            switch (sort)
            {
                case SortingModel.Latest:
                    result = x => x.GiftIdeaId;
                    descending = true;
                    break;
                case SortingModel.Oldest:
                    result = x => x.GiftIdeaId;
                    descending = false;
                    break;
                case SortingModel.BestRated:
                    result = x => x.LikesCounter;
                    descending = true;
                    break;
                case SortingModel.WorstRated:
                    result = x => x.LikesCounter;
                    descending = false;
                    break;
                default:
                    result = x => x.GiftIdeaId;
                    descending = true;
                    break;
            }
            return result;
        }

        public Expression<Func<GiftIdea, bool>> PrepareFilter(FilterModel filterModel)
        {
            Expression<Func<GiftIdea, bool>> result = x => true;
            if (!String.IsNullOrEmpty(filterModel.Author))
            {
                Expression<Func<GiftIdea, bool>> filterDelegatAuthor = x => x.Author == filterModel.Author;
                result = result.And(filterDelegatAuthor);
            }

            if (!String.IsNullOrEmpty(filterModel.GiftName))
            {
                Expression<Func<GiftIdea, bool>> filterDelegatName = x => x.Name.Contains(filterModel.GiftName);
                result = result.And(filterDelegatName);
            }

            if (filterModel.categoryID > -1)
            {
                Expression<Func<GiftIdea, bool>> filterDelegatCategory = x => x.GiftIdeaCategory.Any(y => y.CategoryId == filterModel.categoryID);
                result = result.And(filterDelegatCategory);
            }

            return result;

        }
    }
}
