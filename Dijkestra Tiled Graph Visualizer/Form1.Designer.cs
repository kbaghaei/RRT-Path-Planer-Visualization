/*
    RRT Path Planner Visualizer
    A Program that gets an image that consists of black and white colors.
    which represent walkable areas of a map(white) and its obstacles(black)
    and gets two points and finds a path(not optimal neccessarilly between
    the two given points using RRT algorithm(does not guarantee finding a path).
 
    Copyright (C) 2016  Kourosh Teimouri Baghaei
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace Dijkestra_Tiled_Graph_Visualizer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenGraph = new System.Windows.Forms.Button();
            this.btnDrawPath = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenGraph
            // 
            this.btnGenGraph.Location = new System.Drawing.Point(640, 3);
            this.btnGenGraph.Name = "btnGenGraph";
            this.btnGenGraph.Size = new System.Drawing.Size(132, 23);
            this.btnGenGraph.TabIndex = 0;
            this.btnGenGraph.Text = "Nothing :D";
            this.btnGenGraph.UseVisualStyleBackColor = true;
            this.btnGenGraph.Click += new System.EventHandler(this.btnGenGraph_Click);
            // 
            // btnDrawPath
            // 
            this.btnDrawPath.Location = new System.Drawing.Point(502, 3);
            this.btnDrawPath.Name = "btnDrawPath";
            this.btnDrawPath.Size = new System.Drawing.Size(132, 23);
            this.btnDrawPath.TabIndex = 1;
            this.btnDrawPath.Text = "Draw Path";
            this.btnDrawPath.UseVisualStyleBackColor = true;
            this.btnDrawPath.Click += new System.EventHandler(this.btnDrawPath_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(421, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDrawPath);
            this.Controls.Add(this.btnGenGraph);
            this.Name = "Form1";
            this.Text = "RRT Path Planner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenGraph;
        private System.Windows.Forms.Button btnDrawPath;
        private System.Windows.Forms.Button btnClear;
    }
}

