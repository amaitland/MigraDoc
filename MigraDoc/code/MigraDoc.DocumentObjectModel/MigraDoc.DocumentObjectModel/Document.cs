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

using System;
using MigraDoc.DocumentObjectModel.Visitors;
using PdfSharp.Core.Enums;

namespace MigraDoc.DocumentObjectModel
{
  /// <summary>
  /// Represents a MigraDoc document.
  /// </summary>
  public sealed class Document : DocumentObject, IVisitable
  {
    /// <summary>
    /// Initializes a new instance of the Document class.
    /// </summary>
    public Document()
    {
      this.styles = new Styles(this);
    }

    #region Methods
    /// <summary>
    /// Creates a deep copy of this object.
    /// </summary>
    public new Document Clone()
    {
      return (Document)DeepCopy();
    }

    /// <summary>
    /// Implements the deep copy of the object.
    /// </summary>
    protected override object DeepCopy()
    {
      Document document = (Document)base.DeepCopy();
      if (document.info != null)
      {
        document.info = document.info.Clone();
        document.info.parent = document;
      }
      if (document.styles != null)
      {
        document.styles = document.styles.Clone();
        document.styles.parent = document;
      }
      if (document.sections != null)
      {
        document.sections = document.sections.Clone();
        document.sections.parent = document;
      }
      return document;
    }

    /// <summary>
    /// Internal function used by renderers to bind this instance to it. 
    /// </summary>
    public void BindToRenderer(object renderer)
    {
      //if (this.renderer != null && this.renderer != renderer)
      if (this.renderer != null && renderer != null && !Object.ReferenceEquals(this.renderer, renderer))
      {
        throw new InvalidOperationException("The document is already bound to another renderer. " +
          "A MigraDoc document can be rendered by only one renderer, because the rendering process " +
          "modifies its internal structure. If you want to render a MigraDoc document  on different renderers, " +
          "you must create a copy of it using the Clone function.");
      }
      this.renderer = renderer;
    }
    object renderer;

    /// <summary>
    /// Indicates whether the document is bound to a renderer. A bound document must not be modified anymore.
    /// Modifying it leads to undefined results of the rendering process.
    /// </summary>
    public bool IsBoundToRenderer
    {
      get { return this.renderer != null; }
    }

    /// <summary>
    /// Adds a new section to the document.
    /// </summary>
    public Section AddSection()
    {
      return this.Sections.AddSection();
    }

    /// <summary>
    /// Adds a new style to the document styles.
    /// </summary>
    /// <param name="name">Name of the style.</param>
    /// <param name="baseStyle">Name of the base style.</param>
    public Style AddStyle(string name, string baseStyle)
    {
      if (name == null || baseStyle == null)
        throw new ArgumentNullException(name == null ? "name" : "baseStyle");
      if (name == "" || baseStyle == "")
        throw new ArgumentException(name == "" ? "name" : "baseStyle");

      return this.Styles.AddStyle(name, baseStyle);
    }

    /// <summary>
    /// Adds a new section to the document.
    /// </summary>
    public void Add(Section section)
    {
      this.Sections.Add(section);
    }

    /// <summary>
    /// Adds a new style to the document styles.
    /// </summary>
    public void Add(Style style)
    {
      this.Styles.Add(style);
    }
    #endregion

    #region Properties

    /// <summary>
    /// Gets the last section of the document, or null, if the document has no sections.
    /// </summary>
    public Section LastSection
    {
      get
      {
        return (this.sections != null && this.sections.Count > 0) ?
          this.sections.LastObject as Section : null;
      }
    }

    /// <summary>
    /// Gets or sets a comment associated with this object.
    /// </summary>
    public string Comment
    {
      get { return this.comment; }
      set { this.comment = value; }
    }

	internal string comment;

    /// <summary>
    /// Gets the document info.
    /// </summary>
    public DocumentInfo Info
    {
      get
      {
        if (this.info == null)
          this.info = new DocumentInfo(this);

        return info;
      }
      set
      {
        SetParent(value);
        this.info = value;
      }
    }
    
    internal DocumentInfo info;

    /// <summary>
    /// Gets or sets the styles of the document.
    /// </summary>
    public Styles Styles
    {
      get
      {
        if (this.styles == null)
          this.styles = new Styles(this);

        return this.styles;
      }
      set
      {
        SetParent(value);
        this.styles = value;
      }
    }
    
    internal Styles styles;

    /// <summary>
    /// Gets or sets the default tab stop position.
    /// </summary>
    public Unit DefaultTabStop
    {
      get { return this.defaultTabStop; }
      set { this.defaultTabStop = value; }
    }
    
    internal Unit defaultTabStop = Unit.NullValue;

    /// <summary>
    /// Gets the default page setup.
    /// </summary>
    public PageSetup DefaultPageSetup
    {
      get { return PageSetup.DefaultPageSetup; }
    }

    /// <summary>
    /// Gets or sets the location of the Footnote.
    /// </summary>
	public FootnoteLocation? FootnoteLocation
    {
      get { return this.footnoteLocation; }
      set { this.footnoteLocation = value; }
    }
    
    internal FootnoteLocation? footnoteLocation;

    /// <summary>
    /// Gets or sets the rule which is used to determine the footnote number on a new page.
    /// </summary>
    public FootnoteNumberingRule? FootnoteNumberingRule
    {
      get { return this.footnoteNumberingRule; }
      set { this.footnoteNumberingRule = value; }
    }

	internal FootnoteNumberingRule? footnoteNumberingRule;

    /// <summary>
    /// Gets or sets the type of number which is used for the footnote.
    /// </summary>
    public FootnoteNumberStyle? FootnoteNumberStyle
    {
      get { return this.footnoteNumberStyle; }
      set { this.footnoteNumberStyle = value; }
    }

	internal FootnoteNumberStyle? footnoteNumberStyle;

    /// <summary>
    /// Gets or sets the starting number of the footnote.
    /// </summary>
    public int FootnoteStartingNumber
    {
      get { return this.footnoteStartingNumber.GetValueOrDefault(); }
      set { this.footnoteStartingNumber = value; }
    }
    
    internal int? footnoteStartingNumber;

    /// <summary>
    /// Gets or sets the path for images used by the document.
    /// </summary>
    public string ImagePath
    {
      get { return this.imagePath; }
      set { this.imagePath = value; }
    }

	internal string imagePath;

    /// <summary>
    /// Gets or sets a value indicating whether to use the CMYK color model when rendered as PDF.
    /// </summary>
    public bool UseCmykColor
    {
		get { return this.useCmykColor.GetValueOrDefault(); }
      set { this.useCmykColor = value; }
    }

	internal bool? useCmykColor;

    /// <summary>
    /// Gets the sections of the document.
    /// </summary>
    public Sections Sections
    {
      get
      {
        if (this.sections == null)
          this.sections = new Sections(this);
        return this.sections;
      }
      set
      {
        SetParent(value);
        this.sections = value;
      }
    }
    
    internal Sections sections;
    #endregion

    /// <summary>
    /// Gets the DDL file name.
    /// </summary>
    public string DdlFile
    {
      get { return this.ddlFile; }
    }
    internal string ddlFile = "";

    #region Internal

    /// <summary>
    /// Allows the visitor object to visit the document object and all it's child objects.
    /// </summary>
    void IVisitable.AcceptVisitor(DocumentObjectVisitor visitor, bool visitChildren)
    {
      visitor.VisitDocument(this);
      if (visitChildren)
      {
        ((IVisitable)Styles).AcceptVisitor(visitor, visitChildren);
        ((IVisitable)Sections).AcceptVisitor(visitor, visitChildren);
      }
    }
    #endregion
  }
}
