using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.PillPressRegistry.Public.Models
{
    /// <summary>
    /// ViewModel transforms.
    /// </summary>
    public static class CustomProductExtensions
    {
        /// <summary>
        /// Convert a given voteQuestion to a ViewModel
        /// </summary>        
        public static ViewModels.CustomProduct ToViewModel(this MicrosoftDynamicsCRMbcgovCustomproduct customProduct)
        {
            ViewModels.CustomProduct result = null;
            if (customProduct != null)
            {
                result = new ViewModels.CustomProduct();
                if (customProduct.BcgovCustomproductid != null)
                {
                    result.id = customProduct.BcgovCustomproductid;
                }
                result.productdescriptionandintendeduse = customProduct.BcgovProductdescriptionandintendeduse;                
            }
            return result;
        }

        public static void CopyValues(this MicrosoftDynamicsCRMbcgovCustomproduct to, ViewModels.CustomProduct from)
        {
            to.BcgovProductdescriptionandintendeduse = from.productdescriptionandintendeduse;
        }

    }
}
