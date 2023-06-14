Public Class APOExportBuildFiles
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    ' Change Request 07-AT-0005 
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim rpt_queryname As String = Request.QueryString("rpt_queryname")
        Dim startDate As Date = Convert.ToDateTime(Request.QueryString("startdate"))
        Dim expView As New DataView
        expView = Wyeth.Alf.WyethImportHelper.getExportReports()

        expView.RowFilter = "rpt_queryname='" & rpt_queryname & "'"

                Dim MyExport As New Exporter
                MyExport.Delimiter = Convert.ToString(expView.Item(0).Item("rpt_delimiter"))
                MyExport.QueryName = expView.Item(0).Item("rpt_queryName")
                MyExport.ExportFileName = expView.Item(0).Item("rpt_expfilename")
                MyExport.RemoteFTPDir = expView.Item(0).Item("rpt_exportDironserver")
        MyExport.StartDate = startDate
                MyExport.Execute()
        Response.Write(MyExport.Log)


    End Sub

End Class
