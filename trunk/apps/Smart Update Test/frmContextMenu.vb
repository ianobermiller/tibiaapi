Public Class frmContextMenu

    Private Function OnContextMenuClick(ByVal eventId As Integer) As Boolean
        Select Case eventId
            Case 1000
                MessageBox.Show("TibiaAPI Set Outfit clicked.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Case 1001
                MessageBox.Show("TibiaAPI Party Action clicked.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Case 1002
                MessageBox.Show("TibiaAPI Copy Name clicked.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Case 1003
                MessageBox.Show("TibiaAPI Trade With clicked.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Case 1004
                MessageBox.Show("TibiaAPI Look clicked.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Select
    End Function

    Private Sub btnAddSetOutfit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSetOutfit.Click
        client.ContextMenu.AddContextMenu(1000, "TibiaAPI Set Outfit", Tibia.Constants.ContextMenuType.SetOutfitContextMenu, chkIncludeSeperator.Checked)
    End Sub

    Private Sub btnRemoveSetOutfit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSetOutfit.Click
        client.ContextMenu.RemoveContextMenu(1000, "TibiaAPI Set Outfit", Tibia.Constants.ContextMenuType.SetOutfitContextMenu, chkIncludeSeperator.Checked)
    End Sub

    Private Sub btnAddPartyAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPartyAction.Click
        client.ContextMenu.AddContextMenu(1001, "TibiaAPI Party Action", Tibia.Constants.ContextMenuType.PartyActionContextMenu, chkIncludeSeperator.Checked)
    End Sub

    Private Sub btnRemovePartyAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePartyAction.Click
        client.ContextMenu.RemoveContextMenu(1001, "TibiaAPI Party Action", Tibia.Constants.ContextMenuType.PartyActionContextMenu, chkIncludeSeperator.Checked)
    End Sub

    Private Sub btnAddCopyName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCopyName.Click
        client.ContextMenu.AddContextMenu(1002, "TibiaAPI Copy Name", Tibia.Constants.ContextMenuType.CopyNameContextMenu, chkIncludeSeperator.Checked)
    End Sub

    Private Sub btnRemoveCopyName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveCopyName.Click
        client.ContextMenu.RemoveContextMenu(1002, "TibiaAPI Copy Name", Tibia.Constants.ContextMenuType.CopyNameContextMenu, chkIncludeSeperator.Checked)
    End Sub

    Private Sub btnAddTradeWith_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTradeWith.Click
        client.ContextMenu.AddContextMenu(1003, "TibiaAPI Trade With", Tibia.Constants.ContextMenuType.TradeWithContextMenu, chkIncludeSeperator.Checked)
    End Sub

    Private Sub btnRemoveTradeWith_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTradeWith.Click
        client.ContextMenu.RemoveContextMenu(1003, "TibiaAPI Trade With", Tibia.Constants.ContextMenuType.TradeWithContextMenu, chkIncludeSeperator.Checked)
    End Sub

    Private Sub btnAddLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLook.Click
        client.ContextMenu.AddContextMenu(1004, "TibiaAPI Look", Tibia.Constants.ContextMenuType.LookContextMenu, chkIncludeSeperator.Checked)
    End Sub

    Private Sub btnRemoveLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveLook.Click
        client.ContextMenu.RemoveContextMenu(1004, "TibiaAPI Look", Tibia.Constants.ContextMenuType.LookContextMenu, chkIncludeSeperator.Checked)
    End Sub

    Private Sub btnRemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAll.Click
        client.ContextMenu.RemoveAll()
    End Sub

    Private Sub frmContextMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If client.LoggedIn Then
            AddHandler client.ContextMenu.Click, AddressOf OnContextMenuClick
        Else
            MessageBox.Show("You must be logged in to test Context Menus. Please login and reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub
End Class