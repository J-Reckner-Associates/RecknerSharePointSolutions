using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace RecknerSharePointSolutions.ParTimeAverages
{
    [ToolboxItemAttribute(false)]
    public class ParTimeAverages : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/RecknerSharePointSolutions/ParTimeAverages/ParTimeAveragesUserControl.ascx";

        int _totalHoursTreshhold = 28;
        string _sortExpression = "Averages DESC";
        string _filterExpression = "";
        int _backToMonths = 0;
        DateTime _startDate = new DateTime(2012,12,3);
        
        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public int TotalHoursTreshold { get { return _totalHoursTreshhold; } set { _totalHoursTreshhold = value; } }


        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public string FilterExpression { get { return _filterExpression; } set { _filterExpression = value; } }
        
        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public string SortExpression { get { return _sortExpression; } set { _sortExpression = value; } }


        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public int BackToMonths { get { return _backToMonths; } set { _backToMonths = value; } }

        //Retroactive Obamacare start date
        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; } }


        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
