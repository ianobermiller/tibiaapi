Public Class frmPlayer

    Private Sub frmPlayer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not client.LoggedIn Then
            MessageBox.Show("You must be logged in to test this feature. Please login and reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        Else
            If player Is Nothing Then
                player = client.GetPlayer
                UpdatePlayerInfo()
            End If
        End If
    End Sub

    Private Sub UpdatePlayerInfo()
        lblAxe.Text = player.Axe.ToString
        lblAxePercent.Text = player.Axe_Percent.ToString
        Dim cap As Double = Convert.ToDouble(player.Cap) / 100
        lblCap.Text = cap.ToString
        lblClub.Text = player.Club.ToString
        lblClubPercent.Text = player.Club_Percent.ToString
        lblDistance.Text = player.Distance.ToString
        lblDistancePercent.Text = player.Distance_Percent.ToString
        lblExp.Text = player.Exp.ToString
        lblFishing.Text = player.Fishing.ToString
        lblFishingPercent.Text = player.Fishing_Percent.ToString
        lblFist.Text = player.Fist.ToString
        lblFistPercent.Text = player.Fist_Percent.ToString
        lblFlags.Text = player.Flags.ToString
        lblGoToX.Text = player.GoTo_X.ToString
        lblGoToY.Text = player.GoTo_Y.ToString
        lblGoToZ.Text = player.GoTo_Z.ToString
        lblGreenSquare.Text = player.GreenSquare.ToString
        lblHealth.Text = player.HP.ToString
        lblHealthMax.Text = player.HP_Max.ToString
        lblId.Text = player.Id.ToString
        lblLevel.Text = player.Level.ToString
        lblLevelPercent.Text = player.Level_Percent.ToString
        lblMagic.Text = player.MagicLevel.ToString
        lblMagicPercent.Text = player.MagicLevel_Percent.ToString
        lblMana.Text = player.Mana.ToString
        lblManaMax.Text = player.Mana_Max.ToString
        lblRedSquare.Text = player.RedSquare.ToString
        lblShielding.Text = player.Shielding.ToString
        lblShieldingPercent.Text = player.Shielding_Percent.ToString
        lblSoul.Text = player.Soul.ToString
        lblStamina.Text = player.Stamina.ToString
        lblSword.Text = player.Sword.ToString
        lblSwordPercent.Text = player.Sword_Percent.ToString
        lblTargetBListId.Text = player.Target_BList_ID.ToString
        lblTargetId.Text = player.Target_ID.ToString
        lblTargetBListType.Text = player.Target_BList_Type.ToString
        lblWhiteSquare.Text = player.WhiteSquare.ToString
        lblWorldName.Text = player.WorldName.ToString
        lblX.Text = player.X.ToString
        lblY.Text = player.Y.ToString
        lblZ.Text = player.Z.ToString
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpdatePlayerInfo()
    End Sub
End Class