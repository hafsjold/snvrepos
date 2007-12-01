Imports System
Imports EnvDTE
Imports EnvDTE80
Imports System.Diagnostics
Imports System.Data
Imports System.Xml
Imports wssGetList

Public Module Module1
    Sub InsertGUID()
        Dim textSelection As EnvDTE.TextSelection
        Dim MyGuid As System.Guid = System.Guid.NewGuid()

        textSelection = CType(DTE.ActiveDocument.Selection(), EnvDTE.TextSelection)
        textSelection.Text = MyGuid.ToString ''System.DateTime.Now.ToLongTimeString()
    End Sub
    Sub _spmha()

        Dim textSelection As EnvDTE.TextSelection
        textSelection = CType(DTE.ActiveDocument.Selection(), EnvDTE.TextSelection)

        Dim myList As wssGetList.getList = New wssGetList.getList("http://hd16.hafsjold.dk", "administrator", "m733", "hd16")
        Dim dsItems As System.Data.DataSet = myList.getListData("ProPurList")
        Dim rows As DataTable = dsItems.Tables("row")

        For Each row As DataRow In rows.Rows

            Dim Id As Integer = CType(row("ows_ID"), Integer)
            Dim ListName As String = CType(row("ows_Title"), String)
            Dim FieldName As String = CType(row("ows_FieldName"), String)
            Dim SysName As String = CType(row("ows_SysName"), String)
            Dim DisplayNameDK As String = CType(row("ows_DisplayNameDK"), String)
            Dim DisplayNameUK As String = CType(row("ows_DisplayNameUK"), String)
            Dim Guid As String = CType(row("ows_GUID0"), String)

        Next row

        'textSelection.Text = FieldName

    End Sub

    Sub _mhaXML()
        'Dim SysName As String = "Alice"
        'Dim DisplayNameDK As String = "Alice value DK"
        'Dim DisplayNameUK As String = "Alice value UK"
        'Dim FieldName As String = "Alice comment"

        Const FILENAME_DK = "Resource1.da-dk.resx"
        Const FILENAME_UK = "Resource1.resx"

        Dim XMLFileDK As TextDocument = DTE.Documents.Item(FILENAME_DK).Object("TextDocument")
        Dim XMLFileDKtextSel As TextSelection = XMLFileDK.Selection
        XMLFileDKtextSel.SelectAll()
        Dim docDK As System.Xml.XmlDocument = New System.Xml.XmlDocument()
        docDK.LoadXml(XMLFileDKtextSel.Text)
        XMLFileDKtextSel = Nothing

        Dim XMLFileUK As TextDocument = DTE.Documents.Item(FILENAME_UK).Object("TextDocument")
        Dim XMLFileUKtextSel As TextSelection = XMLFileUK.Selection
        XMLFileUKtextSel.SelectAll()
        Dim docUK As System.Xml.XmlDocument = New System.Xml.XmlDocument()
        docUK.LoadXml(XMLFileUKtextSel.Text)
        XMLFileUKtextSel = Nothing

        Dim myList As wssGetList.getList = New wssGetList.getList("http://hd16.hafsjold.dk", "administrator", "m733", "hd16")
        Dim dsItems As System.Data.DataSet = myList.getListData("ProPurList")
        Dim rows As DataTable = dsItems.Tables("row")

        For Each row As DataRow In rows.Rows

            Dim Id As Integer = CType(row("ows_ID"), Integer)
            Dim ListName As String = CType(row("ows_Title"), String)
            Dim FieldName As String = CType(row("ows_FieldName"), String)
            Dim SysName As String = CType(row("ows_SysName"), String)
            Dim DisplayNameDK As String = CType(row("ows_DisplayNameDK"), String)
            Dim DisplayNameUK As String = CType(row("ows_DisplayNameUK"), String)
            Dim Guid As String = CType(row("ows_GUID0"), String)

            Dim dataDK As System.Xml.XmlElement = createDataElement(docDK, SysName, DisplayNameDK, FieldName)
            Dim dataUK As System.Xml.XmlElement = createDataElement(docUK, SysName, DisplayNameUK, FieldName)
        Next row

        Dim starteditPntDK As EditPoint = XMLFileDK.StartPoint.CreateEditPoint()
        Dim endeditPntDK As EditPoint = XMLFileDK.StartPoint.CreateEditPoint
        starteditPntDK.StartOfDocument()
        endeditPntDK.EndOfDocument()
        starteditPntDK.Delete(endeditPntDK)
        starteditPntDK.Insert(docDK.OuterXml)

        Dim starteditPntUK As EditPoint = XMLFileUK.StartPoint.CreateEditPoint()
        Dim endeditPntUK As EditPoint = XMLFileUK.StartPoint.CreateEditPoint
        starteditPntUK.StartOfDocument()
        endeditPntUK.EndOfDocument()
        starteditPntUK.Delete(endeditPntUK)
        starteditPntUK.Insert(docUK.OuterXml)

    End Sub

    Private Function createDataElement( _
            ByRef pdoc As System.Xml.XmlDocument, _
            ByVal pname As String, _
            ByVal pvalue As String, _
            Optional ByVal pcomment As String = Nothing _
        ) As System.Xml.XmlElement

        Dim data As System.Xml.XmlElement = pdoc.CreateElement("data")
        data.SetAttribute("name", pname)
        data.SetAttribute("xml:space", "preserve")

        Dim value As System.Xml.XmlElement = pdoc.CreateElement("value")
        value.InnerText = pvalue
        data.AppendChild(value)

        If Not pcomment = Nothing Then
            Dim comment As System.Xml.XmlElement = pdoc.CreateElement("comment")
            comment.InnerText = pcomment
            data.AppendChild(comment)
        End If

        Dim root As System.Xml.XmlNode = pdoc.SelectSingleNode("//root")
        Dim old_data As System.Xml.XmlNode = root.SelectSingleNode("//data[@name='" & pname & "']")
        If old_data Is Nothing Then
            root.AppendChild(data)
        Else
            root.ReplaceChild(data, old_data)
        End If

        Return data
    End Function

    Sub _mha()
        Dim ModleFile As TextDocument = DTE.Documents.Item("ModleFile.txt").Object("TextDocument")
        Dim TextFile1 As TextDocument = DTE.Documents.Item("TextFile1.txt").Object("TextDocument")

        Dim ModleFiletextSel As TextSelection = ModleFile.Selection
        Dim TextFile1editPnt As EditPoint = TextFile1.StartPoint.CreateEditPoint()

        ' Position insertion point at beginning of "Line 2".
        ModleFiletextSel.StartOfDocument()
        ModleFiletextSel.LineDown(False, 2)
        ModleFiletextSel.LineDown(True, 1)
        Dim source As String = ModleFiletextSel.Text.ToString()
        ModleFiletextSel.Copy()

        TextFile1editPnt.StartOfDocument()
        TextFile1editPnt.LineDown(5)
        TextFile1editPnt.Paste()
    End Sub

    Sub ReplacePatternExample()
        Dim objTextDoc As TextDocument
        Dim objTxtPt As TextPoint
        Dim iCtr As Integer
        Dim strAnswer As String

        ' Create a new text file.
        DTE.ItemOperations.NewFile("General\Text File")

        ' Set object references, create an EditPoint.
        Dim objSel As TextSelection = DTE.ActiveDocument.Selection
        objTextDoc = DTE.ActiveDocument.Object("TextDocument")
        objTxtPt = objTextDoc.StartPoint.CreateEditPoint

        ' Insert ten lines of text.
        For iCtr = 1 To 10
            objTxtPt.Insert("This is a test." & Chr(13))
        Next iCtr

        strAnswer = MsgBox("Ready to replace 'test' with 'done deal'?", vbYesNo + vbQuestion)

        ' Looks for a specified word pattern and then replaces it with 
        ' another.
        If strAnswer = vbYes Then
            objSel.SelectAll()
            objSel.ReplacePattern("test", "done deal", EnvDTE.vsFindOptions.vsFindOptionsRegularExpression, )
        Else
            MsgBox("Okay, no replacements made.")
        End If
    End Sub

    Sub CreateExample()
        ' Open a solution before running this example.
        'Dim obj As Document
        'obj = DTE.ActiveDocument.Object("Document")

        'Dim obj2 As TextDocument
        'obj2 = DTE.ActiveDocument.Object("TextDocument")

        'Dim obj3 As TextDocument
        'obj3 = obj.Object("TextDocument")

        'Dim obj4 As TextDocument = CType(obj.Object(), TextDocument)



        Dim docs As Documents
        Dim soln As Solution
        Dim proj As Project
        Dim pname As String
        Dim pitem As ProjectItem
        Dim iname As String
        Dim dname As String
        Dim doc As Document
        Dim doc2 As TextDocument

        docs = DTE.Documents
        doc = DTE.Documents.Item("TextFile1.txt")
        doc2 = DTE.Documents.Item("TextFile1.txt").Object("TextDocument")
        ''doc2 = CType(doc.Object(), TextDocument)


        soln = DTE.Solution


        ' Find the specified project.
        proj = soln.Projects.Item(1)
        pname = proj.FullName.ToString
        pitem = proj.ProjectItems.Item(1)
        iname = pitem.Name
        doc = pitem.Document
        dname = doc.Name


    End Sub


    Sub EditPointExample()
        'Before running this example, open a text document.
        Dim objTD As TextDocument
        Dim objEP As EditPoint

        objTD = DTE.ActiveDocument.Object("TextDocument")
        objEP = objTD.StartPoint.CreateEditPoint
        objEP.Insert("Hello")

        DTE.ItemOperations.NewFile()
        Dim textDoc As TextDocument = CType(DTE.ActiveDocument.Object(), TextDocument)
        Dim editPnt As EditPoint = textDoc.StartPoint.CreateEditPoint()
        Dim i As Integer
        For i = 1 To 10
            editPnt.Insert("Line " & i.ToString() & vbCrLf)
        Next

        If MsgBox("Reverse the order of the lines?", MsgBoxStyle.YesNo) = _
            MsgBoxResult.Yes Then
            Dim textSel As TextSelection = textDoc.Selection

            ' Position insertion point at beginning of "Line 2".
            textSel.StartOfDocument()
            textSel.LineDown()

            ' Reverse the order of the lines by cutting "Line 2", 
            ' pasting it at the start of the document, and then cutting
            ' "Line 3" and pasting it at the start of the document,
            ' and so on.
            For i = 1 To 9
                textSel.LineDown(True)
                textSel.Cut()
                editPnt.StartOfDocument()
                editPnt.Paste()
            Next
        End If

    End Sub

End Module

