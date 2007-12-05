Imports System
Imports EnvDTE
Imports EnvDTE80
Imports System.Text.RegularExpressions
Imports System.Diagnostics


Public Module Module2

    ' A list of folder names, file names, and extensions that we do not want to add
    '  to the solution.
    Dim excludedExtensions As New System.Collections.Specialized.StringCollection
    Dim outputWindowPaneTitle As String = "Add directory as solution folder report"

    ' Function to filter out folder names, file names, and extensions that we do not 
    '  want to add to the solution.
    Function IsFileExcluded(ByVal filePath As String) As Boolean
        Dim extension As String
        Dim fileName As String

        extension = System.IO.Path.GetExtension(filePath)
        extension = extension.ToLower()

        fileName = System.IO.Path.GetFileName(filePath)
        fileName = fileName.ToLower()

        If (excludedExtensions.Contains(extension)) Then
            Return True
        Else
            If (excludedExtensions.Contains(fileName)) Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    ' Macro to import a folder on disk into a solution folder structure.
    ' Before running this macro, you must:
    ' 1) Change the path pointed to by startFolder to a path on your computer.
    '    You could also call this macro through the command window, supplying
    '    a start path argument.
    ' 2) Ensure that a solution file is open and saved to disk.
    Sub CopyGenerateSlnFolderOnDirStructure(Optional ByVal LISTNAME As String = "")
        Const FIND_DIR0 = "C:\_Provinsa\TEMPLATE\FEATURES\"
        Const REPLACE_DIR0 = "C:\_Provinsa\ProvPur\ProvPur\TEMPLATE\FEATURES\"
        Const PROJECT_DIR = "C:\_Provinsa\ProvPur\ProvPur\"

        If (String.IsNullOrEmpty(LISTNAME)) Then
            LISTNAME = InputBox("Enter the LISTNANE")
            If (String.IsNullOrEmpty(LISTNAME)) Then
                Return
            End If
        End If

        ''Dim LISTNAME As String = "Document"
        Dim TEMP_NAME As String = "#NAME#List"
        Dim PROJ_NAME As String = FolderPathSubstitute(TEMP_NAME, LISTNAME, "xx", "xx", PROJECT_DIR)
        Dim FIND_DIR As String = FIND_DIR0 & TEMP_NAME
        Dim REPLACE_DIR As String = REPLACE_DIR0 & PROJ_NAME

        excludedExtensions = New System.Collections.Specialized.StringCollection
        ' If you do not want a file with a particular extension or name
        '  to be added, then add that extension or name to this list:
        excludedExtensions.Add(".obj")
        excludedExtensions.Add(".ilk")
        excludedExtensions.Add(".pch")
        excludedExtensions.Add(".pdb")
        excludedExtensions.Add(".exe")
        excludedExtensions.Add(".dll")
        excludedExtensions.Add(".sbr")
        excludedExtensions.Add(".lib")
        excludedExtensions.Add(".exp")
        excludedExtensions.Add(".bsc")
        excludedExtensions.Add(".tlb")
        excludedExtensions.Add(".ncb")
        excludedExtensions.Add(".sln")
        excludedExtensions.Add(".suo")
        excludedExtensions.Add(".vcproj")
        excludedExtensions.Add(".vbproj")
        excludedExtensions.Add(".csproj")
        excludedExtensions.Add(".vjsproj")
        excludedExtensions.Add(".msi")
        excludedExtensions.Add("_svn")

        Dim currentProject As ProjectItem = AddFolder2Project(ProjectPathSubstitute(FIND_DIR, LISTNAME, FIND_DIR, REPLACE_DIR, PROJECT_DIR))
        CopyFolder2(currentProject, FIND_DIR, LISTNAME, FIND_DIR, REPLACE_DIR, PROJECT_DIR)

    End Sub

    Function FolderPathSubstitute(ByVal s As String, ByVal LISTNAME As String, ByVal FIND_DIR As String, ByVal REPLACE_DIR As String, ByVal PROJECT_DIR As String) As String
        Dim s1 = s.Replace(FIND_DIR, REPLACE_DIR)
        Dim s2 = s1.Replace("#NAME#", LISTNAME)
        Return s2
    End Function
    Function ProjectPathSubstitute(ByVal s As String, ByVal LISTNAME As String, ByVal FIND_DIR As String, ByVal REPLACE_DIR As String, ByVal PROJECT_DIR As String) As String
        Dim s1 = s.Replace(FIND_DIR, REPLACE_DIR)
        Dim s2 = s1.Replace("#NAME#", LISTNAME)
        Dim s3 = s2.Replace(PROJECT_DIR, "")
        Return s3
    End Function


    Sub CopyFolder2(ByVal currentProject As ProjectItem, ByVal currentPath As String, ByVal LISTNAME As String, ByVal FIND_DIR As String, ByVal REPLACE_DIR As String, ByVal PROJECT_DIR As String)
        Dim folders As String()
        Dim files As String()
        Dim file As String
        Dim folder As String

        folders = System.IO.Directory.GetDirectories(currentPath)
        files = System.IO.Directory.GetFiles(currentPath)

        ' Examine all the files within the folder.
        For Each file In files
            If (Not IsFileExcluded(file)) Then
                Try
                    System.IO.File.Copy(file, FolderPathSubstitute(file, LISTNAME, FIND_DIR, REPLACE_DIR, PROJECT_DIR))
                    Dim pitem As ProjectItem = currentProject.ProjectItems().AddFromFile(FolderPathSubstitute(file, LISTNAME, FIND_DIR, REPLACE_DIR, PROJECT_DIR))
                    pitem.Open()
                    Dim tmpFile As TextDocument = DTE.Documents.Item(pitem.Name).Object("TextDocument")
                    Dim tmpFiletextSel As TextSelection = tmpFile.Selection
                    tmpFiletextSel.SelectAll()
                    Dim s1 As String = tmpFiletextSel.Text.Replace("#NAME#", LISTNAME)

                    Dim stMyGuid = System.Guid.NewGuid().ToString
                    Dim s As String = s1.Replace("#GUID-FEATURE#", stMyGuid)

                    Dim starteditPntFIELDS As EditPoint = tmpFile.StartPoint.CreateEditPoint()
                    Dim endeditPntFIELDS As EditPoint = tmpFile.StartPoint.CreateEditPoint
                    starteditPntFIELDS.StartOfDocument()
                    endeditPntFIELDS.EndOfDocument()
                    starteditPntFIELDS.Delete(endeditPntFIELDS)
                    starteditPntFIELDS.Insert(s)
                Catch
                    Dim outputWindowPane As EnvDTE.OutputWindowPane
                    outputWindowPane = GetOutputWindowPane(outputWindowPaneTitle, True)
                    outputWindowPane.OutputString("The item """ + file + """may have not been added to the solution." + vbLf)
                End Try
            End If
        Next

        ' Examine all the subfolders.
        For Each folder In folders
            If (Not IsFileExcluded(folder)) Then
                Dim currentProject2 As ProjectItem = AddFolder2Project(ProjectPathSubstitute(folder, LISTNAME, FIND_DIR, REPLACE_DIR, PROJECT_DIR))
                CopyFolder2(currentProject2, folder, LISTNAME, FIND_DIR, REPLACE_DIR, PROJECT_DIR)
            End If
        Next
    End Sub

    Function AddFolder2Project(Optional ByVal ProjectPath As String = "TEMPLATE2/FEATURES/TEST2") As ProjectItem
        Dim proj As Project = DTE.ActiveSolutionProjects(0)
        Dim folders() As String = Regex.Split(ProjectPath, "\\")
        Dim p(folders.Length - 1) As ProjectItem
        Dim pitems As ProjectItems = proj.ProjectItems()
        Dim pitem As ProjectItem

        For i As Integer = 0 To folders.Length - 1
            For Each pitem In pitems
                If pitem.Name = folders(i) Then
                    pitems = pitem.ProjectItems
                    p(i) = pitem
                    Exit For
                End If
            Next
            If p(i) Is Nothing Then
                If i = 0 Then
                    p(i) = proj.ProjectItems.AddFolder(folders(i))
                Else
                    p(i) = p(i - 1).ProjectItems.AddFolder(folders(i))
                End If
                pitems = p(i).ProjectItems
            End If
        Next i
        Return p(p.Length - 1)
    End Function

End Module

