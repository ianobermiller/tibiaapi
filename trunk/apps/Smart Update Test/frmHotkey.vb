Imports Tibia

Public Class frmHotkey

    Private Sub btnGetHotkeys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetHotkeys.Click
        For i As Integer = 0 To Addresses.Hotkey.MaxHotkeys - 1
            Dim h As New Objects.Hotkey(client, i)
            If h.ObjectId > 0 Or h.Text IsNot String.Empty Then
                lvHotkeys.Items.Add(New ListViewItem(New String() {h.SendAutomatically.ToString, h.Text.ToString, h.ObjectId.ToString, h.ObjectUseType.ToString, h.Shortcut.ToString}))
            End If
        Next
    End Sub
End Class