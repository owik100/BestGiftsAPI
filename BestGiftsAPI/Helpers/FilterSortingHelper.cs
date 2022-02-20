using BestGiftsAPI.Entities;
using BestGiftsAPI.Other_Models;
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
    }
}
