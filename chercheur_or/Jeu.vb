﻿Public Class Jeu
    Dim taille As Integer
    Const largeurTile As Integer = 35, hauteurTile As Integer = 35

    Dim couleurSelect As Color = ColorTranslator.FromHtml("0x008fff")
    Dim couleur As Color = ColorTranslator.FromHtml("0xffffff")

    Dim terrain(taille, taille) As Integer
    Dim terrainAffichage(taille, taille) As PictureBox

    Dim tableauJoueurs() As String
    Dim tableauJoueursVirtuels() As Boolean
    Dim indiceJoueurEnCours As Integer

    Dim tableauStatsJoueurs As List(Of Panel)

    Dim nbPepites As Integer, nbDynamites As Integer

    Enum CaseTerrain As Integer
        HERBE
        TERRE
        DYNAMITE
        DYNAMITE_TROUVEE
        PEPITE
        PEPITE_TROUVEE
    End Enum

    Dim dynamitesRestantesImg As PictureBox
    Dim dynamitesRestantes As Label
    Dim pepitesRestantesImg As PictureBox
    Dim pepitesRestantes As Label
    Dim herbesRestantesImg As PictureBox
    Dim herbesRestantes As Label

    Dim utiliserDynamite As CheckBox

    Dim chargementEffectue As Boolean = False
    Public Sub chargerDonnees(ByVal joueurs() As String, ByVal joueursVirtuels() As Boolean, ByVal tailleTerrain As Integer, ByVal pepites As Integer, ByVal dynamites As Integer)
        nbPepites = pepites
        nbDynamites = dynamites
        taille = tailleTerrain
        tableauJoueurs = joueurs
        tableauJoueursVirtuels = joueursVirtuels
        ReDim terrain(taille, taille)
        ReDim terrainAffichage(taille, taille)

        chargementEffectue = True
    End Sub

    Private Sub chargementFenetre(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (chargementEffectue = False) Then
            Me.Close()
        End If

        Randomize()

        'Configuration de la fenêtre de base
        Me.Text = "Le Chercheur d'Or"
        Me.Width = 1024
        Me.Height = 768

        genererJeu()
    End Sub

    Public Sub genererJeu()
        'Création du terrain
        For i As Integer = 0 To taille - 1
            For j As Integer = 0 To taille - 1
                terrain(i, j) = CaseTerrain.HERBE 'On remplit le terrain d'herbe

                terrainAffichage(i, j) = New PictureBox
                terrainAffichage(i, j).Location = New Point(i * largeurTile, j * hauteurTile)
                terrainAffichage(i, j).Size = New Size(largeurTile, hauteurTile)
                terrainAffichage(i, j).Image = My.Resources.herbe
                terrainAffichage(i, j).Visible = True
                AddHandler terrainAffichage(i, j).MouseHover, AddressOf Me.survolSouris
                AddHandler terrainAffichage(i, j).Click, AddressOf Me.clic
                Me.Controls.Add(terrainAffichage(i, j))
            Next
        Next

        'Génération aléatoire des pépites & dynamites
        genererPepites()
        genererDynamites()

        'Génération des statistiques (création dans le code pour plus de propreté et de précision)
        genererStatsGlobales()

        tableauStatsJoueurs = New List(Of Panel)

        For i As Integer = 0 To Int(tableauJoueurs.Length) - 1
            creerJoueur(i)
        Next
        tableauStatsJoueurs(0).BackColor = couleurSelect

        utiliserDynamite = New CheckBox
        utiliserDynamite.Appearance = Appearance.Button
        utiliserDynamite.Image = My.Resources.dynamite
        utiliserDynamite.Location = New Point((taille + 1) * largeurTile, hauteurTile * 2 + hauteurTile / 4)
        utiliserDynamite.Size = New Size(largeurTile * 1.5, hauteurTile * 1.5)
        utiliserDynamite.Visible = True
        Me.Controls.Add(utiliserDynamite)
    End Sub

    Private Sub creerJoueur(ByVal numJoueur)
        tableauStatsJoueurs.Add(New Panel)
        Dim origine As Point = New Point((taille + 1) * largeurTile, hauteurTile * (numJoueur + 1) * 2 + 5 * numJoueur + hauteurTile * 2)
        tableauStatsJoueurs(numJoueur).Name = "joueur" & numJoueur
        tableauStatsJoueurs(numJoueur).Location = origine
        tableauStatsJoueurs(numJoueur).Size = New Size(largeurTile * 6 + 5, hauteurTile * 2)
        tableauStatsJoueurs(numJoueur).BackColor = couleur
        tableauStatsJoueurs(numJoueur).Visible = True

        Dim nomJoueur As Label = New Label
        nomJoueur.Name = "joueur" & numJoueur
        nomJoueur.Text = tableauJoueurs(numJoueur)
        nomJoueur.Location = New Point(origine.X, origine.Y)
        nomJoueur.Size = New Size(largeurTile * 6, hauteurTile)
        nomJoueur.Visible = True
        nomJoueur.BackColor = tableauStatsJoueurs(numJoueur).BackColor
        nomJoueur.TextAlign = ContentAlignment.MiddleCenter
        tableauStatsJoueurs(numJoueur).Controls.Add(nomJoueur)
        Me.Controls.Add(nomJoueur)

        Dim dynamitesImg As PictureBox = New PictureBox
        dynamitesImg.Location = New Point(origine.X, origine.Y + hauteurTile)
        dynamitesImg.Size = New Size(largeurTile, hauteurTile)
        dynamitesImg.Image = My.Resources.dynamite
        dynamitesImg.Visible = True
        tableauStatsJoueurs(numJoueur).Controls.Add(dynamitesImg)
        Me.Controls.Add(dynamitesImg)

        Dim dynamites As Label = New Label
        dynamites.Text = "0"
        dynamites.Name = "dynamites_joueur" & numJoueur
        dynamites.Location = New Point(origine.X + largeurTile, origine.Y + hauteurTile)
        dynamites.Size = New Size(largeurTile, hauteurTile)
        dynamites.Visible = True
        dynamites.BackColor = tableauStatsJoueurs(numJoueur).BackColor
        dynamites.TextAlign = ContentAlignment.MiddleCenter
        tableauStatsJoueurs(numJoueur).Controls.Add(dynamites)
        Me.Controls.Add(dynamites)

        Dim herbesImg As PictureBox = New PictureBox
        herbesImg.Location = New Point(origine.X + largeurTile * 2, origine.Y + hauteurTile)
        herbesImg.Size = New Size(largeurTile, hauteurTile)
        herbesImg.Image = My.Resources.herbe
        herbesImg.Visible = True
        tableauStatsJoueurs(numJoueur).Controls.Add(herbesImg)
        Me.Controls.Add(herbesImg)

        Dim herbes As Label = New Label
        herbes.Name = "herbes_joueur" & numJoueur
        herbes.Text = "0"
        herbes.Location = New Point(origine.X + largeurTile * 3, origine.Y + hauteurTile)
        herbes.Size = New Size(largeurTile, hauteurTile)
        herbes.Visible = True
        herbes.BackColor = tableauStatsJoueurs(numJoueur).BackColor
        herbes.TextAlign = ContentAlignment.MiddleCenter
        tableauStatsJoueurs(numJoueur).Controls.Add(herbes)
        Me.Controls.Add(herbes)

        Dim pepitesImg As PictureBox = New PictureBox
        pepitesImg.Location = New Point(origine.X + largeurTile * 4, origine.Y + hauteurTile)
        pepitesImg.Size = New Size(largeurTile, hauteurTile)
        pepitesImg.Image = My.Resources._or
        pepitesImg.Visible = True
        tableauStatsJoueurs(numJoueur).Controls.Add(pepitesImg)
        Me.Controls.Add(pepitesImg)

        Dim pepites As Label = New Label
        pepites.Text = "0"
        pepites.Name = "pepites_joueur" & numJoueur
        pepites.Location = New Point(origine.X + largeurTile * 5, origine.Y + hauteurTile)
        pepites.Size = New Size(largeurTile, hauteurTile)
        pepites.BackColor = tableauStatsJoueurs(numJoueur).BackColor
        pepites.Visible = True
        pepites.TextAlign = ContentAlignment.MiddleCenter
        tableauStatsJoueurs(numJoueur).Controls.Add(pepites)
        Me.Controls.Add(pepites)

        tableauStatsJoueurs(numJoueur).Visible = True
        Me.Controls.Add(tableauStatsJoueurs(numJoueur))
    End Sub

    Private Sub genererStatsGlobales()
        dynamitesRestantesImg = New PictureBox
        dynamitesRestantesImg.Location = New Point((taille + 1) * largeurTile, hauteurTile)
        dynamitesRestantesImg.Size = New Size(largeurTile, hauteurTile)
        dynamitesRestantesImg.Image = My.Resources.dynamite
        dynamitesRestantesImg.Visible = True
        Me.Controls.Add(dynamitesRestantesImg)

        dynamitesRestantes = New Label
        dynamitesRestantes.Text = nbDynamites.ToString
        dynamitesRestantes.Location = New Point((taille + 2) * largeurTile, hauteurTile)
        dynamitesRestantes.Size = New Size(largeurTile, hauteurTile)
        dynamitesRestantes.Visible = True
        dynamitesRestantes.TextAlign = ContentAlignment.MiddleCenter
        Me.Controls.Add(dynamitesRestantes)

        herbesRestantesImg = New PictureBox
        herbesRestantesImg.Location = New Point((taille + 3) * largeurTile, hauteurTile)
        herbesRestantesImg.Size = New Size(largeurTile, hauteurTile)
        herbesRestantesImg.Image = My.Resources.herbe
        herbesRestantesImg.Visible = True
        Me.Controls.Add(herbesRestantesImg)

        herbesRestantes = New Label
        herbesRestantes.Text = (taille * taille).ToString
        herbesRestantes.Location = New Point((taille + 4) * largeurTile, hauteurTile)
        herbesRestantes.Size = New Size(largeurTile, hauteurTile)
        herbesRestantes.Visible = True
        herbesRestantes.TextAlign = ContentAlignment.MiddleCenter
        Me.Controls.Add(herbesRestantes)

        pepitesRestantesImg = New PictureBox
        pepitesRestantesImg.Location = New Point((taille + 5) * largeurTile, hauteurTile)
        pepitesRestantesImg.Size = New Size(largeurTile, hauteurTile)
        pepitesRestantesImg.Image = My.Resources._or
        pepitesRestantesImg.Visible = True
        Me.Controls.Add(pepitesRestantesImg)

        pepitesRestantes = New Label
        pepitesRestantes.Text = nbPepites.ToString
        pepitesRestantes.Location = New Point((taille + 6) * largeurTile, hauteurTile)
        pepitesRestantes.Size = New Size(largeurTile, hauteurTile)
        pepitesRestantes.Visible = True
        pepitesRestantes.TextAlign = ContentAlignment.MiddleCenter
        Me.Controls.Add(pepitesRestantes)
    End Sub

    Private Sub genererPepites()
        Dim compteur As Integer = nbPepites
        While compteur > 0 'Tant qu'on a pas posé toutes les pépites on continue
            Dim blocPepite As Integer
            If (compteur > 4) Then
                blocPepite = Int(4 * Rnd() + 2)
            ElseIf (compteur = 4 Or compteur = 3 Or compteur = 2) Then
                blocPepite = compteur
            End If

            Dim xTMP As Integer = Int((taille - 1) * Rnd()), yTMP As Integer = Int((taille - 1) * Rnd())

            If (terrain(xTMP, yTMP) = CaseTerrain.HERBE And terrain(xTMP + 1, yTMP) = CaseTerrain.HERBE) Then 'On pose les deux premières pépites
                terrain(xTMP, yTMP) = CaseTerrain.PEPITE
                terrain(xTMP + 1, yTMP) = CaseTerrain.PEPITE
                compteur -= 2
                If (blocPepite >= 3 Or compteur = 1) Then 'On en pose une troisième si le bloc est de 3
                    If (terrain(xTMP, yTMP + 1) = CaseTerrain.HERBE) Then
                        terrain(xTMP, yTMP + 1) = CaseTerrain.PEPITE
                        compteur -= 1
                        If (blocPepite >= 4 Or compteur = 1) Then 'Idem avec une quatrième
                            If (terrain(xTMP + 1, yTMP + 1) = CaseTerrain.HERBE) Then
                                terrain(xTMP + 1, yTMP + 1) = CaseTerrain.PEPITE
                                compteur -= 1
                            End If
                        End If
                    End If
                End If
            End If

        End While
    End Sub

    Private Sub genererDynamites()
        Dim compteur As Integer = nbDynamites
        While compteur > 0
            Dim xTMP As Integer = Int((taille - 1) * Rnd()), yTMP As Integer = Int((taille - 1) * Rnd())
            If (terrain(xTMP, yTMP) = CaseTerrain.HERBE) Then
                terrain(xTMP, yTMP) = CaseTerrain.DYNAMITE
                compteur -= 1
            End If
        End While
    End Sub

    Dim coordonneesPrec As Point
    Private Sub survolSouris(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim caseSurvole As PictureBox = sender

        'Permet d'effacer la case précédente sur laquelle la souris est passé
        If (coordonneesPrec.X >= 0 And coordonneesPrec.X < taille And coordonneesPrec.Y >= 0 And coordonneesPrec.Y < taille And terrain(coordonneesPrec.X, coordonneesPrec.Y) <> CaseTerrain.TERRE And terrain(coordonneesPrec.X, coordonneesPrec.Y) <> CaseTerrain.PEPITE_TROUVEE And terrain(coordonneesPrec.X, coordonneesPrec.Y) <> CaseTerrain.DYNAMITE_TROUVEE) Then
            terrainAffichage(coordonneesPrec.X, coordonneesPrec.Y).Image = My.Resources.herbe
        End If

        'Affiche un curseur sur la case que l'on survole
        If (terrain(Int(caseSurvole.Location.X / largeurTile), Int(caseSurvole.Location.Y / hauteurTile)) <> CaseTerrain.TERRE And terrain(Int(caseSurvole.Location.X / largeurTile), Int(caseSurvole.Location.Y / hauteurTile)) <> CaseTerrain.PEPITE_TROUVEE And terrain(Int(caseSurvole.Location.X / largeurTile), Int(caseSurvole.Location.Y / hauteurTile)) <> CaseTerrain.DYNAMITE_TROUVEE) Then
            caseSurvole.Image = My.Resources.survol
            coordonneesPrec = New Point(Int(caseSurvole.Location.X / largeurTile), Int(caseSurvole.Location.Y / hauteurTile))
        End If
    End Sub

    Private Sub jouerIa()
        Dim positionX As Integer = Int((taille - 1) * Rnd())
        Dim positionY As Integer = Int((taille - 1) * Rnd())
        Dim objet = terrainAffichage(positionX, positionY)
        clic(objet, EventArgs.Empty)
    End Sub

    Private Sub clic(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim changerJoueur As Boolean = False
        'On regarde si on doit utiliser une dynamite
        If (utiliserDynamite.Checked) Then
            Dim tabTMP() As Control = Me.Controls.Find("dynamites_joueur" & indiceJoueurEnCours, True)
            'On regarde si on a des dynamites
            If (Int(tabTMP(0).Text) > 0) Then
                changerJoueur = dynamiter(sender, e)
            Else
                changerJoueur = piocher(sender, e)
            End If
        Else
            changerJoueur = piocher(sender, e)
        End If

        'On regarde s'il reste des pépites sinon le jeu est fini
        If (Int(pepitesRestantes.Text) <= 0 And tableauJoueursVirtuels(indiceJoueurEnCours) = False) Then
            jeuFini()
        ElseIf (Int(pepitesRestantes.Text) <= 0 And tableauJoueursVirtuels(indiceJoueurEnCours) = True) Then 'évite les double-fenêtres de scores
            Return
        End If

        'On change de joueur
        If (changerJoueur = True) Then
            If (indiceJoueurEnCours >= tableauJoueurs.Length - 1) Then 'Si c'était le dernier joueur alors on revient au début
                tableauStatsJoueurs(indiceJoueurEnCours).BackColor = couleur
                indiceJoueurEnCours = 0
                tableauStatsJoueurs(indiceJoueurEnCours).BackColor = couleurSelect
            Else
                tableauStatsJoueurs(indiceJoueurEnCours).BackColor = couleur
                indiceJoueurEnCours += 1
                tableauStatsJoueurs(indiceJoueurEnCours).BackColor = couleurSelect
            End If
            'Si c'est une IA
            If tableauJoueursVirtuels(indiceJoueurEnCours) = True Then
                jouerIa()
            End If
        ElseIf changerJoueur = False And tableauJoueursVirtuels(indiceJoueurEnCours) = True Then
            jouerIa()
        End If
    End Sub

    Private Function piocher(ByVal sender As System.Object, ByVal e As System.EventArgs) As Boolean
        Dim caseCliquee As PictureBox = sender
        Dim x As Integer = Int(caseCliquee.Location.X / largeurTile), y As Integer = Int(caseCliquee.Location.Y / hauteurTile)

        If (terrain(x, y) = CaseTerrain.PEPITE Or terrain(x, y) = CaseTerrain.DYNAMITE Or terrain(x, y) = CaseTerrain.HERBE) Then
            herbesRestantes.Text = Int(herbesRestantes.Text) - 1
            Dim tabTMP() As Control = Me.Controls.Find("herbes_joueur" & indiceJoueurEnCours, True)
            tabTMP(0).Text = Int(tabTMP(0).Text) + 1
        End If

        Select Case terrain(x, y)
            Case CaseTerrain.PEPITE
                terrain(x, y) = CaseTerrain.PEPITE_TROUVEE
                terrainAffichage(x, y).Image = My.Resources._or
                pepitesRestantes.Text = Int(pepitesRestantes.Text) - 1
                Dim tabTMP() As Control = Me.Controls.Find("pepites_joueur" & indiceJoueurEnCours, True)
                tabTMP(0).Text = Int(tabTMP(0).Text) + 1
                Return False
            Case CaseTerrain.DYNAMITE
                terrain(x, y) = CaseTerrain.DYNAMITE_TROUVEE
                terrainAffichage(x, y).Image = My.Resources.dynamite
                dynamitesRestantes.Text = Int(dynamitesRestantes.Text) - 1
                Dim tabTMP() As Control = Me.Controls.Find("dynamites_joueur" & indiceJoueurEnCours, True)
                tabTMP(0).Text = Int(tabTMP(0).Text) + 1
                Return True
            Case CaseTerrain.HERBE
                terrain(x, y) = CaseTerrain.TERRE
                terrainAffichage(x, y).Image = My.Resources.terre
                Return True
        End Select

        Return False
    End Function

    Private Function dynamiter(ByVal sender As System.Object, ByVal e As System.EventArgs) As Boolean
        Dim caseCliquee As PictureBox = sender
        Dim x As Integer = Int(caseCliquee.Location.X / largeurTile), y As Integer = Int(caseCliquee.Location.Y / hauteurTile)
        'On définit notre carré impact
        Dim origineX As Integer = x - 1, origineY As Integer = y - 1
        Dim finX As Integer = x + 1, finY As Integer = y + 1

        'On règle les débordements (hors du terrain)
        If (origineX < 0) Then
            origineX = 0
        End If
        If (origineY < 0) Then
            origineY = 0
        End If
        If (finY > taille - 1) Then
            finY = taille - 1
        End If
        If (finX > taille - 1) Then
            finX = taille - 1
        End If

        'Si aucune case n'est de l'herbe, alors on ne peut pas dynamiter
        Dim auMoinsUneCaseCliquable As Boolean = False
        Dim changerJoueur As Boolean = True
        For i As Integer = origineX To finX
            For j As Integer = origineY To finY
                If terrain(x, y) = CaseTerrain.HERBE Or terrain(x, y) = CaseTerrain.PEPITE Or terrain(x, y) = CaseTerrain.DYNAMITE Then
                    auMoinsUneCaseCliquable = True
                    If terrain(x, y) = CaseTerrain.PEPITE Then
                        changerJoueur = False
                    End If
                End If
            Next
        Next
        If (auMoinsUneCaseCliquable = False) Then
            Return False
        End If

        'Dynamiter revient à piocher plusieurs cases
        For i As Integer = origineX To finX
            For j As Integer = origineY To finY
                piocher(terrainAffichage(i, j), System.EventArgs.Empty)
            Next
        Next
        Dim tabTMP() As Control = Me.Controls.Find("dynamites_joueur" & indiceJoueurEnCours, True)
        tabTMP(0).Text = Int(tabTMP(0).Text) - 1
        utiliserDynamite.Checked = False

        Return changerJoueur
    End Function

    Private Sub jeuFini()
        Dim chaineScores As String = ""
        Dim scores(tableauJoueurs.Length) As Single
        'On calcule les scores pour chaque joueur
        For i As Integer = 0 To tableauJoueurs.Length - 2
            Dim tabTMP() As Control = Me.Controls.Find("pepites_joueur" & i, True)
            Dim pepites As Integer = Int(tabTMP(0).Text)
            Dim tabTMP2() As Control = Me.Controls.Find("herbes_joueur" & i, True)
            Dim herbes As Integer = Int(tabTMP2(0).Text)
            Dim tabTMP3() As Control = Me.Controls.Find("dynamites_joueur" & i, True)
            Dim dynamites As Integer = Int(tabTMP3(0).Text)

            Dim score As Single = (herbes + dynamites + pepites) / (pepites + 1) 'Permet de ne pas diviser par 0
            If (i <> 0) Then
                If (scores(i - 1) > score) Then
                    scores(i) = scores(i - 1)
                    scores(i - 1) = score

                    Dim chaineTMP As String = tableauJoueurs(i - 1)
                    tableauJoueurs(i - 1) = tableauJoueurs(i)
                    tableauJoueurs(i) = chaineTMP
                Else
                    scores(i) = score
                End If
            Else
                scores(i) = score
            End If
        Next
        For i As Integer = 0 To tableauJoueurs.Length - 2
            If (i <> 0) Then
                If (scores(i - 1) = scores(i)) Then
                    chaineScores += i.ToString
                Else
                    chaineScores += (i + 1).ToString
                End If
            Else
                chaineScores += (i + 1).ToString
            End If
            chaineScores += ". " & tableauJoueurs(i) & " : "
            chaineScores += scores(i) & vbCrLf
        Next
        MessageBox.Show(chaineScores)
        Me.Close()
    End Sub

    'Efface le contenu de la fenêtre pour éviter les superpositions si on relance la partie
    Private Sub fermeture() Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
End Class
