<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Menu
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.boutonCredits = New System.Windows.Forms.Button()
        Me.panelAccueil = New System.Windows.Forms.Panel()
        Me.boutonRegles = New System.Windows.Forms.Button()
        Me.boutonJouerAccueil = New System.Windows.Forms.Button()
        Me.panelCreationGrille = New System.Windows.Forms.Panel()
        Me.panelDynamique = New System.Windows.Forms.Panel()
        Me.labelJoueurs = New System.Windows.Forms.Label()
        Me.comboboxNbDynamites = New System.Windows.Forms.ComboBox()
        Me.comboboxNbPepites = New System.Windows.Forms.ComboBox()
        Me.comboboxTailleGrille = New System.Windows.Forms.ComboBox()
        Me.labelNbDynamite = New System.Windows.Forms.Label()
        Me.labelNbPepites = New System.Windows.Forms.Label()
        Me.labelTailleGrille = New System.Windows.Forms.Label()
        Me.boutonJouerCreationGrille = New System.Windows.Forms.Button()
        Me.boutonMenuPrincipal = New System.Windows.Forms.Button()
        Me.panelAccueil.SuspendLayout()
        Me.panelCreationGrille.SuspendLayout()
        Me.SuspendLayout()
        '
        'boutonCredits
        '
        Me.boutonCredits.Location = New System.Drawing.Point(355, 339)
        Me.boutonCredits.Name = "boutonCredits"
        Me.boutonCredits.Size = New System.Drawing.Size(23, 23)
        Me.boutonCredits.TabIndex = 4
        Me.boutonCredits.Text = "?"
        Me.boutonCredits.UseVisualStyleBackColor = True
        '
        'panelAccueil
        '
        Me.panelAccueil.Controls.Add(Me.boutonRegles)
        Me.panelAccueil.Controls.Add(Me.boutonJouerAccueil)
        Me.panelAccueil.Location = New System.Drawing.Point(88, 34)
        Me.panelAccueil.Name = "panelAccueil"
        Me.panelAccueil.Size = New System.Drawing.Size(187, 213)
        Me.panelAccueil.TabIndex = 5
        '
        'boutonRegles
        '
        Me.boutonRegles.Location = New System.Drawing.Point(29, 116)
        Me.boutonRegles.Name = "boutonRegles"
        Me.boutonRegles.Size = New System.Drawing.Size(144, 41)
        Me.boutonRegles.TabIndex = 7
        Me.boutonRegles.Text = "Règles"
        Me.boutonRegles.UseVisualStyleBackColor = True
        '
        'boutonJouerAccueil
        '
        Me.boutonJouerAccueil.Location = New System.Drawing.Point(29, 37)
        Me.boutonJouerAccueil.Name = "boutonJouerAccueil"
        Me.boutonJouerAccueil.Size = New System.Drawing.Size(144, 41)
        Me.boutonJouerAccueil.TabIndex = 5
        Me.boutonJouerAccueil.Text = "Jouer"
        Me.boutonJouerAccueil.UseVisualStyleBackColor = True
        '
        'panelCreationGrille
        '
        Me.panelCreationGrille.Controls.Add(Me.panelDynamique)
        Me.panelCreationGrille.Controls.Add(Me.labelJoueurs)
        Me.panelCreationGrille.Controls.Add(Me.comboboxNbDynamites)
        Me.panelCreationGrille.Controls.Add(Me.comboboxNbPepites)
        Me.panelCreationGrille.Controls.Add(Me.comboboxTailleGrille)
        Me.panelCreationGrille.Controls.Add(Me.labelNbDynamite)
        Me.panelCreationGrille.Controls.Add(Me.labelNbPepites)
        Me.panelCreationGrille.Controls.Add(Me.labelTailleGrille)
        Me.panelCreationGrille.Controls.Add(Me.boutonJouerCreationGrille)
        Me.panelCreationGrille.Location = New System.Drawing.Point(51, 24)
        Me.panelCreationGrille.Name = "panelCreationGrille"
        Me.panelCreationGrille.Size = New System.Drawing.Size(273, 299)
        Me.panelCreationGrille.TabIndex = 6
        '
        'panelDynamique
        '
        Me.panelDynamique.Location = New System.Drawing.Point(23, 150)
        Me.panelDynamique.Name = "panelDynamique"
        Me.panelDynamique.Size = New System.Drawing.Size(236, 117)
        Me.panelDynamique.TabIndex = 8
        '
        'labelJoueurs
        '
        Me.labelJoueurs.AutoSize = True
        Me.labelJoueurs.Location = New System.Drawing.Point(20, 123)
        Me.labelJoueurs.Name = "labelJoueurs"
        Me.labelJoueurs.Size = New System.Drawing.Size(44, 13)
        Me.labelJoueurs.TabIndex = 7
        Me.labelJoueurs.Text = "Joueurs"
        '
        'comboboxNbDynamites
        '
        Me.comboboxNbDynamites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboboxNbDynamites.FormattingEnabled = True
        Me.comboboxNbDynamites.Location = New System.Drawing.Point(139, 80)
        Me.comboboxNbDynamites.Name = "comboboxNbDynamites"
        Me.comboboxNbDynamites.Size = New System.Drawing.Size(121, 21)
        Me.comboboxNbDynamites.TabIndex = 6
        '
        'comboboxNbPepites
        '
        Me.comboboxNbPepites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboboxNbPepites.FormattingEnabled = True
        Me.comboboxNbPepites.Location = New System.Drawing.Point(139, 47)
        Me.comboboxNbPepites.Name = "comboboxNbPepites"
        Me.comboboxNbPepites.Size = New System.Drawing.Size(121, 21)
        Me.comboboxNbPepites.TabIndex = 5
        '
        'comboboxTailleGrille
        '
        Me.comboboxTailleGrille.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboboxTailleGrille.FormattingEnabled = True
        Me.comboboxTailleGrille.Location = New System.Drawing.Point(139, 14)
        Me.comboboxTailleGrille.Name = "comboboxTailleGrille"
        Me.comboboxTailleGrille.Size = New System.Drawing.Size(121, 21)
        Me.comboboxTailleGrille.TabIndex = 4
        '
        'labelNbDynamite
        '
        Me.labelNbDynamite.AutoSize = True
        Me.labelNbDynamite.Location = New System.Drawing.Point(18, 83)
        Me.labelNbDynamite.Name = "labelNbDynamite"
        Me.labelNbDynamite.Size = New System.Drawing.Size(109, 13)
        Me.labelNbDynamite.TabIndex = 3
        Me.labelNbDynamite.Text = "Nombre de dynamites"
        '
        'labelNbPepites
        '
        Me.labelNbPepites.AutoSize = True
        Me.labelNbPepites.Location = New System.Drawing.Point(18, 51)
        Me.labelNbPepites.Name = "labelNbPepites"
        Me.labelNbPepites.Size = New System.Drawing.Size(96, 13)
        Me.labelNbPepites.TabIndex = 2
        Me.labelNbPepites.Text = "Nombre de pépites"
        '
        'labelTailleGrille
        '
        Me.labelTailleGrille.AutoSize = True
        Me.labelTailleGrille.Location = New System.Drawing.Point(17, 18)
        Me.labelTailleGrille.Name = "labelTailleGrille"
        Me.labelTailleGrille.Size = New System.Drawing.Size(82, 13)
        Me.labelTailleGrille.TabIndex = 1
        Me.labelTailleGrille.Text = "Taille de la grille"
        '
        'boutonJouerCreationGrille
        '
        Me.boutonJouerCreationGrille.Location = New System.Drawing.Point(104, 273)
        Me.boutonJouerCreationGrille.Name = "boutonJouerCreationGrille"
        Me.boutonJouerCreationGrille.Size = New System.Drawing.Size(75, 23)
        Me.boutonJouerCreationGrille.TabIndex = 0
        Me.boutonJouerCreationGrille.Text = "Jouer"
        Me.boutonJouerCreationGrille.UseVisualStyleBackColor = True
        '
        'boutonMenuPrincipal
        '
        Me.boutonMenuPrincipal.Location = New System.Drawing.Point(134, 329)
        Me.boutonMenuPrincipal.Name = "boutonMenuPrincipal"
        Me.boutonMenuPrincipal.Size = New System.Drawing.Size(110, 33)
        Me.boutonMenuPrincipal.TabIndex = 7
        Me.boutonMenuPrincipal.Text = "Menu principal"
        Me.boutonMenuPrincipal.UseVisualStyleBackColor = True
        '
        'Menu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 369)
        Me.Controls.Add(Me.boutonMenuPrincipal)
        Me.Controls.Add(Me.panelCreationGrille)
        Me.Controls.Add(Me.panelAccueil)
        Me.Controls.Add(Me.boutonCredits)
        Me.Name = "Menu"
        Me.Text = "Le chercheur d'or"
        Me.panelAccueil.ResumeLayout(False)
        Me.panelCreationGrille.ResumeLayout(False)
        Me.panelCreationGrille.PerformLayout()
        Me.ResumeLayout(False)

    End Sub



    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Friend WithEvents boutonCredits As System.Windows.Forms.Button


    Friend WithEvents panelAccueil As System.Windows.Forms.Panel
    Friend WithEvents boutonRegles As System.Windows.Forms.Button
    Friend WithEvents boutonJouerAccueil As System.Windows.Forms.Button
    Private Sub solo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Visible = False
    End Sub
    Friend WithEvents panelCreationGrille As System.Windows.Forms.Panel
    Friend WithEvents boutonJouerCreationGrille As System.Windows.Forms.Button
    Friend WithEvents labelNbDynamite As System.Windows.Forms.Label
    Friend WithEvents labelNbPepites As System.Windows.Forms.Label
    Friend WithEvents labelTailleGrille As System.Windows.Forms.Label
    Friend WithEvents labelJoueurs As System.Windows.Forms.Label
    Friend WithEvents comboboxNbDynamites As System.Windows.Forms.ComboBox
    Friend WithEvents comboboxNbPepites As System.Windows.Forms.ComboBox
    Friend WithEvents comboboxTailleGrille As System.Windows.Forms.ComboBox
    Friend WithEvents boutonMenuPrincipal As System.Windows.Forms.Button
    Friend WithEvents panelDynamique As System.Windows.Forms.Panel
End Class
