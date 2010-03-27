Imports Tibia

Public Class frmVIP

    Private Sub btnGetVIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetVIP.Click
        If client.LoggedIn Then
            lvVIP.Items.Clear()
            Dim VIPs As New Objects.VipList(client)
            For Each VIP As Objects.Vip In VIPs
                If VIP.Id = 0 Then Exit For
                lvVIP.Items.Add(New ListViewItem(New String() {VIP.Id.ToString, VIP.Name.ToString, VIP.Status.ToString, VIP.Icon.ToString}))
            Next
        Else
            MessageBox.Show("Please login to perform this action.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class