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

namespace MigraDoc.DocumentObjectModel.Shapes.Charts
{
  /// <summary>
  /// Represents a legend of a chart.
  /// </summary>
  public class Legend : ChartObject, IVisitable
  {
    /// <summary>
    /// Initializes a new instance of the Legend class.
    /// </summary>
    public Legend()
    {
    }

    /// <summary>
    /// Initializes a new instance of the Legend class with the specified parent.
    /// </summary>
    internal Legend(DocumentObject parent) : base(parent) { }

    #region Serialization
    /// <summary>
    /// Creates a deep copy of this object.
    /// </summary>
    public new Legend Clone()
    {
      return (Legend)DeepCopy();
    }

    /// <summary>
    /// Implements the deep copy of the object.
    /// </summary>
    protected override object DeepCopy()
    {
      Legend legend = (Legend)base.DeepCopy();
      if (legend.format != null)
      {
        legend.format = legend.format.Clone();
        legend.format.parent = legend;
      }
      if (legend.lineFormat != null)
      {
        legend.lineFormat = legend.lineFormat.Clone();
        legend.lineFormat.parent = legend;
      }
      return legend;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the style name of the legend's text.
    /// </summary>
    public string Style
    {
      get { return this.style; }
      set { this.style = value; }
    }

	internal string style;

    /// <summary>
    /// Gets the paragraph format of the legend's text.
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
    /// Gets the line format of the legend's border.
    /// </summary>
    public LineFormat LineFormat
    {
      get
      {
        if (this.lineFormat == null)
          this.lineFormat = new LineFormat(this);

        return this.lineFormat;
      }
      set
      {
        SetParent(value);
        this.lineFormat = value;
      }
    }
    
    internal LineFormat lineFormat;
    #endregion


    #region IVisitable Members

    void IVisitable.AcceptVisitor(DocumentObjectVisitor visitor, bool visitChildren)
    {
      visitor.VisitLegend(this);
    }

    #endregion
  }
}
