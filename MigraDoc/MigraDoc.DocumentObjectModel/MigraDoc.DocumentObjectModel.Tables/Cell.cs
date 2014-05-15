#region MigraDoc - Creating Documents on the Fly
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//   Klaus Potzesny (mailto:Klaus.Potzesny@pdfsharp.com)
//   David Stephensen (mailto:David.Stephensen@pdfsharp.com)
//
// Copyright (c) 2001-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://www.migradoc.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion


using MigraDoc.DocumentObjectModel.Visitors;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Shapes.Charts;
using PdfSharp.Core.Enums;
using ChartType = MigraDoc.DocumentObjectModel.Shapes.Charts.ChartType;

namespace MigraDoc.DocumentObjectModel.Tables
{
  /// <summary>
  /// Represents a cell of a table.
  /// </summary>
  public class Cell : DocumentObject, IVisitable
  {
    /// <summary>
    /// Initializes a new instance of the Cell class.
    /// </summary>
    public Cell()
    {
    }

    /// <summary>
    /// Initializes a new instance of the Cell class with the specified parent.
    /// </summary>
    internal Cell(DocumentObject parent) : base(parent) { }

    #region Methods
    /// <summary>
    /// Creates a deep copy of this object.
    /// </summary>
    public new Cell Clone()
    {
      return (Cell)DeepCopy();
    }

    /// <summary>
    /// Implements the deep copy of the object.
    /// </summary>
    protected override object DeepCopy()
    {
      Cell cell = (Cell)base.DeepCopy();
      if (cell.format != null)
      {
        cell.format = cell.format.Clone();
        cell.format.parent = cell;
      }
      if (cell.borders != null)
      {
        cell.borders = cell.borders.Clone();
        cell.borders.parent = cell;
      }
      if (cell.shading != null)
      {
        cell.shading = cell.shading.Clone();
        cell.shading.parent = cell;
      }
      if (cell.elements != null)
      {
        cell.elements = cell.elements.Clone();
        cell.elements.parent = cell;
      }
      return cell;
    }

    /// <summary>
    /// Resets the cached values.
    /// </summary>
    internal override void ResetCachedValues()
    {
      this.row = null;
      this.clm = null;
    }

    /// <summary>
    /// Adds a new paragraph to the cell.
    /// </summary>
    public Paragraph AddParagraph()
    {
      return this.Elements.AddParagraph();
    }

    /// <summary>
    /// Adds a new paragraph with the specified text to the cell.
    /// </summary>
    public Paragraph AddParagraph(string paragraphText)
    {
      return this.Elements.AddParagraph(paragraphText);
    }

    /// <summary>
    /// Adds a new chart with the specified type to the cell.
    /// </summary>
    public Chart AddChart(ChartType type)
    {
      return this.Elements.AddChart(type);
    }

    /// <summary>
    /// Adds a new chart to the cell.
    /// </summary>
    public Chart AddChart()
    {
      return this.Elements.AddChart();
    }

    /// <summary>
    /// Adds a new Image to the cell.
    /// </summary>
    public Image AddImage(string fileName)
    {
      return this.Elements.AddImage(fileName);
    }

    /// <summary>
    /// Adds a new textframe to the cell.
    /// </summary>
    public TextFrame AddTextFrame()
    {
      return this.Elements.AddTextFrame();
    }

    /// <summary>
    /// Adds a new paragraph to the cell.
    /// </summary>
    public void Add(Paragraph paragraph)
    {
      this.Elements.Add(paragraph);
    }

    /// <summary>
    /// Adds a new chart to the cell.
    /// </summary>
    public void Add(Chart chart)
    {
      this.Elements.Add(chart);
    }

    /// <summary>
    /// Adds a new image to the cell.
    /// </summary>
    public void Add(Image image)
    {
      this.Elements.Add(image);
    }

    /// <summary>
    /// Adds a new text frame to the cell.
    /// </summary>
    public void Add(TextFrame textFrame)
    {
      this.Elements.Add(textFrame);
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the table the cell belongs to.
    /// </summary>
    public Table Table
    {
      get
      {
        if (this.table == null)
        {
          Cells cls = this.Parent as Cells;
          if (cls != null)
            this.table = cls.Table;
        }
        return this.table;
      }
    }
    Table table;

    /// <summary>
    /// Gets the column the cell belongs to.
    /// </summary>
    public Column Column
    {
      get
      {
        if (this.clm == null)
        {
          Cells cells = this.Parent as Cells;
          for (int index = 0; index < cells.Count; ++index)
          {
            if (cells[index] == this)
              this.clm = this.Table.Columns[index];
          }
        }
        return this.clm;
      }
    }
    Column clm;

    /// <summary>
    /// Gets the row the cell belongs to.
    /// </summary>
    public Row Row
    {
      get
      {
        if (this.row == null)
        {
          Cells cells = this.Parent as Cells;
          this.row = cells.Row;
        }
        return this.row;
      }
    }
    Row row;

    /// <summary>
    /// Sets or gets the style name.
    /// </summary>
    public string Style
    {
      get { return this.style; }
      set { this.style = value; }
    }
    
    internal string style = null;

    /// <summary>
    /// Gets the ParagraphFormat object of the paragraph.
    /// </summary>
    public ParagraphFormat Format
    {
      get
      {
        if (this.format == null)
          this.format = new ParagraphFormat(this);

        return this.format;
      }
      set
      {
        SetParent(value);
        this.format = value;
      }
    }
    
    internal ParagraphFormat format;

    /// <summary>
    /// Gets or sets the vertical alignment of the cell.
    /// </summary>
    public VerticalAlignment? VerticalAlignment
    {
      get { return this.verticalAlignment; }
      set { this.verticalAlignment = value; }
    }
    
    internal VerticalAlignment? verticalAlignment;

    /// <summary>
    /// Gets the Borders object.
    /// </summary>
    public Borders Borders
    {
      get
      {
        if (this.borders == null)
        {
          if (this.Document == null) // BUG CMYK
            GetType();
          this.borders = new Borders(this);
        }
        return this.borders;
      }
      set
      {
        SetParent(value);
        this.borders = value;
      }
    }
    
    internal Borders borders;

    /// <summary>
    /// Gets the shading object.
    /// </summary>
    public Shading Shading
    {
      get
      {
        if (this.shading == null)
          this.shading = new Shading(this);

        return this.shading;
      }
      set
      {
        SetParent(value);
        this.shading = value;
      }
    }
    
    internal Shading shading;

    /// <summary>
    /// Gets or sets the number of cells to be merged right.
    /// </summary>
    public int MergeRight
    {
      get { return this.mergeRight.GetValueOrDefault(); }
      set { this.mergeRight = value; }
    }
    
    internal int? mergeRight = null;

    /// <summary>
    /// Gets or sets the number of cells to be merged down.
    /// </summary>
    public int MergeDown
    {
      get { return this.mergeDown.GetValueOrDefault(); }
      set { this.mergeDown = value; }
    }
    
    internal int? mergeDown = null;

    /// <summary>
    /// Gets the collection of document objects that defines the cell.
    /// </summary>
    public DocumentElements Elements
    {
      get
      {
        if (this.elements == null)
          this.elements = new DocumentElements(this);

        return this.elements;
      }
      set
      {
        SetParent(value);
        this.elements = value;
      }
    }
    
    internal DocumentElements elements;

    /// <summary>
    /// Gets or sets a comment associated with this object.
    /// </summary>
    public string Comment
    {
      get { return this.comment; }
      set { this.comment = value; }
    }
    
    internal string comment = null;
    #endregion

    #region Internal

	  /// <summary>
    /// Allows the visitor object to visit the document object and it's child objects.
    /// </summary>
    void IVisitable.AcceptVisitor(DocumentObjectVisitor visitor, bool visitChildren)
    {
      visitor.VisitCell(this);

      if (visitChildren && this.elements != null)
        ((IVisitable)this.elements).AcceptVisitor(visitor, visitChildren);
    }

	  
    #endregion
  }
}