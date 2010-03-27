Public Class frmClient

    Private Sub frmClient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateClientInfo()
    End Sub

    Private Sub UpdateClientInfo()
        lblActionState.Text = client.ActionState.ToString
        lblAttackMode.Text = client.AttackMode.ToString
        lblDialogCaption.Text = client.DialogCaption.ToString
        lblDialogPointer.Text = client.DialogPointer.ToString
        lblDialogPosition.Text = client.DialogPosition.ToString
        lblFollowMode.Text = client.FollowMode.ToString
        lblIsDialogOpen.Text = client.IsDialogOpen.ToString
        lblLastSeenCount.Text = client.LastSeenCount.ToString
        lblLastSeenId.Text = client.LastSeenId.ToString
        lblLastSeenText.Text = client.LastSeenText.ToString
        lblLoggedIn.Text = client.LoggedIn.ToString
        lblSafeMode.Text = client.SafeMode.ToString
        lblStatus.Text = client.Status.ToString
        lblStatusBar.Text = client.Statusbar.ToString
        lblVersion.Text = client.Version.ToString
        lblVersionNumber.Text = client.VersionNumber.ToString
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpdateClientInfo()
    End Sub
End Class