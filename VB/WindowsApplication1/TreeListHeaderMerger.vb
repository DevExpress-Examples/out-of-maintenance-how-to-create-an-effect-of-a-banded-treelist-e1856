Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Columns

Namespace WindowsApplication1
	Public Class TreeListHeaderMerger
		Private treeList As TreeList
		Private columnToMerge As TreeListColumn


		Public Sub New(ByVal treeList As TreeList, ByVal columnToMerge As TreeListColumn)
			Me.treeList = treeList
			Me.columnToMerge = columnToMerge
			columnToMerge.OptionsColumn.AllowMove = False
			GetNextColumn().OptionsColumn.AllowMove = False
			AddHandler treeList.CustomDrawColumnHeader, AddressOf treeList_CustomDrawColumnHeader
		End Sub

		Private Function GetNextColumn() As TreeListColumn
			If columnToMerge.VisibleIndex = treeList.VisibleColumns.Count - 1 Then
				Return Nothing
			End If
			Return treeList.VisibleColumns(columnToMerge.VisibleIndex + 1)
		End Function

		Private Sub treeList_CustomDrawColumnHeader(ByVal sender As Object, ByVal e As CustomDrawColumnHeaderEventArgs)
			Dim nextColumn As TreeListColumn = GetNextColumn()
			If nextColumn Is Nothing Then
				Return
			End If
			If e.Column Is nextColumn Then
				e.Handled = True
				Return
			End If
			If e.Column IsNot columnToMerge Then
				Return
			End If
			Dim r As Rectangle = e.ObjectArgs.Bounds
			r.Width = r.Width + nextColumn.VisibleWidth
			e.ObjectArgs.Bounds = r
		End Sub
	End Class
End Namespace
