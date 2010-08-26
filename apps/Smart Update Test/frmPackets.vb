Imports Tibia

Public Class frmPackets

    Private Sub btnClientMemory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClientMemory.Click
        If client.Dll.Pipe IsNot Nothing Then
            client.Dll.DisconnectPipe()
        End If
        Packets.Incoming.TextMessagePacket.Send(client, Constants.TextMessageColor.RedConsole, "TibiAPI Send Packet To Client By Memory")
    End Sub

    Private Sub btnServerMemory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServerMemory.Click
        If client.Dll.Pipe IsNot Nothing Then
            client.Dll.DisconnectPipe()
        End If
        Packets.Outgoing.PlayerSpeechPacket.Send(client, Constants.SpeechType.Whisper, Nothing, "Hello", Constants.ChatChannel.None)
    End Sub

    Private Sub btnClientHookProxy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClientHookProxy.Click
        If hook Is Nothing Then
            hook = New Packets.HookProxy(client)
        Else
            If Not client.Dll.Pipe IsNot Nothing Then
                client.Dll.InitializePipe()
            End If
        End If
        Packets.Incoming.TextMessagePacket.Send(client, Constants.TextMessageColor.RedConsole, "TibiAPI Send Packet To Client By HookProxy")
    End Sub

    Private Sub btnServerHookProxy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServerHookProxy.Click
        If hook Is Nothing Then
            hook = New Packets.HookProxy(client)
        Else
            If Not client.Dll.Pipe IsNot Nothing Then
                client.Dll.InitializePipe()
            End If
        End If
        Packets.Outgoing.PlayerSpeechPacket.Send(client, Constants.SpeechType.Whisper, Nothing, "Hello", Constants.ChatChannel.None)
    End Sub

    Private Sub frmPackets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not client.LoggedIn Then
            MessageBox.Show("You must be logged in to test this feature. Please login and reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub
End Class