using System;
using System.Collections.Generic;

namespace MVCExtensions
{
    public static class HtmlHelper
    {
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, bool disableLabel)
        {
            MvcHtmlString mvc = htmlHelper.DropDownList(name, selectList, optionLabel);
            if (disableLabel)
            {
                string disabledOption = mvc.ToHtmlString();
                int index = disabledOption.IndexOf(optionLabel);
                disabledOption = disabledOption.Insert(index - 1, " disabled");

                return new MvcHtmlString(disabledOption);
            }
            else
            {
                return mvc;
            }
        }
    }
}
