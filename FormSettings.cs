﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCJMapper_V2
{
  partial class FormSettings : Form
  {
    private readonly AppSettings m_owner = null; // owner class - access to settings


    public Boolean Canceled { get; set; }

    public String PasteString { get; set; } // used to copy, paste JS commands

    /// <summary>
    /// ctor - gets the owning class instance
    /// </summary>
    /// <param name="owner"></param>
    public FormSettings( AppSettings owner )
    {
      InitializeComponent( );
      m_owner = owner;
    }


    private void FormSettings_Load( object sender, EventArgs e )
    {
      chkLbActionMaps.Items.Clear( );
      for ( int i = 0; i < ActionMapsCls.ActionMaps.Length; i++ ) {
        chkLbActionMaps.Items.Add( ActionMapsCls.ActionMaps[i] );
      }
      LoadSettings( );
    }


    // Save from app settings into actuals
    private void LoadSettings( )
    {
      // SC path
      txSCPath.Text = m_owner.UserSCPath;
      cbxUsePath.Checked = m_owner.UserSCPathUsed;

      //Ignore Buttons
      txJS1.Text = m_owner.IgnoreJS1;
      txJS2.Text = m_owner.IgnoreJS2;
      txJS3.Text = m_owner.IgnoreJS3;
      txJS4.Text = m_owner.IgnoreJS4;
      txJS5.Text = m_owner.IgnoreJS5;
      txJS6.Text = m_owner.IgnoreJS6;
      txJS7.Text = m_owner.IgnoreJS7;
      txJS8.Text = m_owner.IgnoreJS8;
      txJS9.Text = m_owner.IgnoreJS9;
      txJS10.Text = m_owner.IgnoreJS10;
      txJS11.Text = m_owner.IgnoreJS11;
      txJS12.Text = m_owner.IgnoreJS12;

      // Ignore actionmaps
      for ( int i = 0; i < chkLbActionMaps.Items.Count; i++ ) {
        if ( m_owner.IgnoreActionmaps.Contains( "," + chkLbActionMaps.Items[i].ToString( ) + "," ) ) {
          chkLbActionMaps.SetItemChecked( i, true );
        } else {
          chkLbActionMaps.SetItemChecked( i, false ); // 20161223: fix checked items and Canceled
        }
      }

      // DetectGamepad
      cbxDetectGamepad.Checked = m_owner.DetectGamepad;

      // Use PTU
      cbxPTU.Checked = m_owner.UsePTU;

      // Use CSV Listing
      cbxCSVListing.Checked = m_owner.UseCSVListing;
      cbxListModifiers.Checked = m_owner.ListModifiers;

    }


    // Save the current settings
    private void SaveSettings( )
    {
      // SC path
      m_owner.UserSCPath = txSCPath.Text;
      m_owner.UserSCPathUsed = cbxUsePath.Checked;

      //Ignore Buttons
      m_owner.IgnoreJS1 = txJS1.Text;
      m_owner.IgnoreJS2 = txJS2.Text;
      m_owner.IgnoreJS3 = txJS3.Text;
      m_owner.IgnoreJS4 = txJS4.Text;
      m_owner.IgnoreJS5 = txJS5.Text;
      m_owner.IgnoreJS6 = txJS6.Text;
      m_owner.IgnoreJS7 = txJS7.Text;
      m_owner.IgnoreJS8 = txJS8.Text;
      m_owner.IgnoreJS9 = txJS9.Text;
      m_owner.IgnoreJS10 = txJS10.Text;
      m_owner.IgnoreJS11 = txJS11.Text;
      m_owner.IgnoreJS12 = txJS12.Text;

      // Ignore actionmaps
      String ignore = ",";
      for ( int i = 0; i < chkLbActionMaps.Items.Count; i++ ) {
        if ( chkLbActionMaps.GetItemCheckState( i ) == CheckState.Checked ) {
          ignore += chkLbActionMaps.Items[i].ToString( ) + ",";
        }
      }
      m_owner.IgnoreActionmaps = ignore;

      // DetectGamepad
      if ( m_owner.DetectGamepad != cbxDetectGamepad.Checked ) {
        MessageBox.Show( "Changing the Gamepad option needs a restart of the application !!", "Settings Notification", MessageBoxButtons.OK, MessageBoxIcon.Information );
      }
      m_owner.DetectGamepad = cbxDetectGamepad.Checked;

      // Use PTU
      if ( m_owner.UsePTU != cbxPTU.Checked ) {
        MessageBox.Show( "Changing to / from PTU folders needs a restart of the application !!", "Settings Notification", MessageBoxButtons.OK, MessageBoxIcon.Information );
      }
      m_owner.UsePTU = cbxPTU.Checked;

      // Use CSV Listing
      m_owner.UseCSVListing = cbxCSVListing.Checked;
      m_owner.ListModifiers = cbxListModifiers.Checked;

      m_owner.Save( );
    }


    private void btDone_Click( object sender, EventArgs e )
    {
      SaveSettings( );
      Canceled = false;
      this.Hide( );
    }

    private void btCancel_Click( object sender, EventArgs e )
    {
      LoadSettings( );
      Canceled = true;
      this.Hide( );
    }

    // allow only numbers, blanks and del
    private bool nonNumberEntered = false;

    private void txJS1_KeyDown( object sender, KeyEventArgs e )
    {
      nonNumberEntered = false;
      // Determine whether the keystroke is a number from the top of the keyboard.
      if ( e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9 ) {
        if ( e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9 ) {
          if ( e.KeyCode != Keys.Back ) {
            if ( e.KeyCode != Keys.Space ) {
              nonNumberEntered = true;
            }
          }
        }
      }
      //If shift key was pressed, it's not a number.
      if ( Control.ModifierKeys == Keys.Shift ) {
        nonNumberEntered = true;
      }
    }

    private void txJS1_KeyPress( object sender, KeyPressEventArgs e )
    {
      if ( nonNumberEntered == true ) {
        // Stop the character from being entered into the control since it is non-numerical.
        e.Handled = true;
      }
    }

    private void FormSettings_FormClosing( object sender, FormClosingEventArgs e )
    {
      SaveSettings( );
      if ( e.CloseReason == CloseReason.UserClosing ) {
        e.Cancel = true;
        this.Hide( );
      }
    }

    private void btChooseSCDir_Click( object sender, EventArgs e )
    {
      if ( fbDlg.ShowDialog( this ) == System.Windows.Forms.DialogResult.OK ) {
        txSCPath.Text = fbDlg.SelectedPath;
      }
    }


  }
}
