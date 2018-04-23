using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

namespace WindowsApplication1
{
    public class TreeListHeaderMerger
    {
        private TreeList treeList;
        private TreeListColumn columnToMerge;
        

        public TreeListHeaderMerger(TreeList treeList, TreeListColumn columnToMerge)
        {
            this.treeList = treeList;
            this.columnToMerge = columnToMerge;
            columnToMerge.OptionsColumn.AllowMove = false;
            GetNextColumn().OptionsColumn.AllowMove = false;
            treeList.CustomDrawColumnHeader += treeList_CustomDrawColumnHeader;
        }

        TreeListColumn GetNextColumn()
        {
            if (columnToMerge.VisibleIndex == treeList.VisibleColumns.Count - 1) return null;
            return treeList.VisibleColumns[columnToMerge.VisibleIndex + 1];
        }

        void treeList_CustomDrawColumnHeader(object sender, CustomDrawColumnHeaderEventArgs e)
        {
            TreeListColumn nextColumn = GetNextColumn();
            if (nextColumn == null) return;
            if (e.Column == nextColumn) { e.Handled = true; return; }
            if (e.Column != columnToMerge) return;
            Rectangle r = e.ObjectArgs.Bounds;
            r.Width = r.Width + nextColumn.VisibleWidth;
            e.ObjectArgs.Bounds = r;
        }
    }
}
