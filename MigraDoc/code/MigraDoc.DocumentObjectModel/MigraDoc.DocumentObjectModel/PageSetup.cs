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

using System.Diagnostics;
using PdfSharp.Core.Enums;

namespace MigraDoc.DocumentObjectModel
{
  /// <summary>
  /// Represents the page setup of a section.
  /// </summary>
  public class PageSetup : DocumentObject
  {
    /// <summary>
    /// Initializes a new instance of the PageSetup class.
    /// </summary>
    public PageSetup()
    {
    }

    /// <summary>
    /// Initializes a new instance of the PageSetup class with the specified parent.
    /// </summary>
    internal PageSetup(DocumentObject parent) : base(parent) { }

    #region Methods
    /// <summary>
    /// Creates a deep copy of this object.
    /// </summary>
    public new PageSetup Clone()
    {
      return (PageSetup)DeepCopy();
    }

    /// <summary>
    /// Gets the page's size and height for the given PageFormat.
    /// </summary>
    public static void GetPageSize(PageFormat? pageFormat, out Unit pageWidth, out Unit pageHeight)
    {
      //Sizes in mm:
      pageWidth = 0;
      pageHeight = 0;
      int A0Height = 1189;
      int A0Width = 841;
      int height = 0;
      int width = 0;
      switch (pageFormat)
      {
        case PdfSharp.Core.Enums.PageFormat.A0:
          height = A0Height;
          width = A0Width;
          break;
		case PdfSharp.Core.Enums.PageFormat.A1:
          height = A0Width;
          width = A0Height / 2;
          break;
		case PdfSharp.Core.Enums.PageFormat.A2:
          height = A0Height / 2;
          width = A0Width / 2;
          break;
		case PdfSharp.Core.Enums.PageFormat.A3:
          height = A0Width / 2;
          width = A0Height / 4;
          break;
		case PdfSharp.Core.Enums.PageFormat.A4:
          height = A0Height / 4;
          width = A0Width / 4;
          break;
		case PdfSharp.Core.Enums.PageFormat.A5:
          height = A0Width / 4;
          width = A0Height / 8;
          break;
		case PdfSharp.Core.Enums.PageFormat.A6:
          height = A0Height / 8;
          width = A0Width / 8;
          break;
		case PdfSharp.Core.Enums.PageFormat.B5:
          height = 257;
          width = 182;
          break;
		case PdfSharp.Core.Enums.PageFormat.Letter:
          pageWidth = Unit.FromPoint(612);
          pageHeight = Unit.FromPoint(792);
          break;
		case PdfSharp.Core.Enums.PageFormat.Legal:
          pageWidth = Unit.FromPoint(612);
          pageHeight = Unit.FromPoint(1008);
          break;
		case PdfSharp.Core.Enums.PageFormat.Ledger:
          pageWidth = Unit.FromPoint(1224);
          pageHeight = Unit.FromPoint(792);
          break;
		case PdfSharp.Core.Enums.PageFormat.P11x17:
          pageWidth = Unit.FromPoint(792);
          pageHeight = Unit.FromPoint(1224);
          break;
      }
      if (height > 0)
        pageHeight = Unit.FromMillimeter(height);
      if (width > 0)
        pageWidth = Unit.FromMillimeter(width);
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a value which defines whether the section starts on next, odd or even page.
    /// </summary>
    public BreakType? SectionStart
    {
      get { return this.sectionStart; }
      set { this.sectionStart = value; }
    }
    
    internal BreakType? sectionStart;

    /// <summary>
    /// Gets or sets the page orientation of the section.
    /// </summary>
    public Orientation? Orientation
    {
      get { return this.orientation; }
      set { this.orientation = value; }
    }
    
    internal Orientation? orientation;

    /// <summary>
    /// Gets or sets the page width.
    /// </summary>
    public Unit PageWidth
    {
      get { return this.pageWidth; }
      set { this.pageWidth = value; }
    }
    
    internal Unit pageWidth = Unit.NullValue;

    /// <summary>
    /// Gets or sets the starting number for the first section page.
    /// </summary>
    public int StartingNumber
    {
      get { return this.startingNumber.GetValueOrDefault(); }
      set { this.startingNumber = value; }
    }
    
    internal int? startingNumber;

    /// <summary>
    /// Gets or sets the page height.
    /// </summary>
    public Unit PageHeight
    {
      get { return this.pageHeight; }
      set { this.pageHeight = value; }
    }
    
    internal Unit pageHeight = Unit.NullValue;

    /// <summary>
    /// Gets or sets the top margin of the pages in the section.
    /// </summary>
    public Unit TopMargin
    {
      get { return this.topMargin; }
      set { this.topMargin = value; }
    }
    
    internal Unit topMargin = Unit.NullValue;

    /// <summary>
    /// Gets or sets the bottom margin of the pages in the section.
    /// </summary>
    public Unit BottomMargin
    {
      get { return this.bottomMargin; }
      set { this.bottomMargin = value; }
    }
    
    internal Unit bottomMargin = Unit.NullValue;

    /// <summary>
    /// Gets or sets the left margin of the pages in the section.
    /// </summary>
    public Unit LeftMargin
    {
      get { return this.leftMargin; }
      set { this.leftMargin = value; }
    }
    
    internal Unit leftMargin = Unit.NullValue;

    /// <summary>
    /// Gets or sets the right margin of the pages in the section.
    /// </summary>
    public Unit RightMargin
    {
      get { return this.rightMargin; }
      set { this.rightMargin = value; }
    }
    
    internal Unit rightMargin = Unit.NullValue;

    /// <summary>
    /// Gets or sets a value which defines whether the odd and even pages
    /// of the section have different header and footer.
    /// </summary>
    public bool OddAndEvenPagesHeaderFooter
    {
		get { return this.oddAndEvenPagesHeaderFooter.GetValueOrDefault(); }
      set { this.oddAndEvenPagesHeaderFooter = value; }
    }

	internal bool? oddAndEvenPagesHeaderFooter;

    /// <summary>
    /// Gets or sets a value which define whether the section has a different
    /// first page header and footer.
    /// </summary>
    public bool DifferentFirstPageHeaderFooter
    {
		get { return this.differentFirstPageHeaderFooter.GetValueOrDefault(); }
      set { this.differentFirstPageHeaderFooter = value; }
    }

	internal bool? differentFirstPageHeaderFooter;

    /// <summary>
    /// Gets or sets the distance between the header and the page top
    /// of the pages in the section.
    /// </summary>
    public Unit HeaderDistance
    {
      get { return this.headerDistance; }
      set { this.headerDistance = value; }
    }
    
    internal Unit headerDistance = Unit.NullValue;

    /// <summary>
    /// Gets or sets the distance between the footer and the page bottom
    /// of the pages in the section.
    /// </summary>
    public Unit FooterDistance
    {
      get { return this.footerDistance; }
      set { this.footerDistance = value; }
    }
    
    internal Unit footerDistance = Unit.NullValue;

    /// <summary>
    /// Gets or sets a value which defines whether the odd and even pages
    /// of the section should change left and right margin.
    /// </summary>
    public bool MirrorMargins
    {
		get { return this.mirrorMargins.GetValueOrDefault(); }
      set { this.mirrorMargins = value; }
    }

	internal bool? mirrorMargins;

    /// <summary>
    /// Gets or sets a value which defines whether a page should break horizontally.
    /// Currently only tables are supported.
    /// </summary>
    public bool HorizontalPageBreak
    {
		get { return this.horizontalPageBreak.GetValueOrDefault(); }
      set { this.horizontalPageBreak = value; }
    }

	internal bool? horizontalPageBreak;

    /// <summary>
    /// Gets or sets the page format of the section.
    /// </summary>
    public PageFormat? PageFormat
    {
      get { return this.pageFormat; }
      set { this.pageFormat = value; }
    }
    
    internal PageFormat? pageFormat;

    /// <summary>
    /// Gets or sets a comment associated with this object.
    /// </summary>
    public string Comment
    {
      get { return this.comment; }
      set { this.comment = value; }
    }

	internal string comment;
    #endregion

    /// <summary>
    /// Gets the PageSetup of the previous section, or null, if the page setup belongs 
    /// to the first section.
    /// </summary>
    public PageSetup PreviousPageSetup()
    {
      Section section = Parent as Section;
      if (section != null)
      {
        section = section.PreviousSection();
        if (section != null)
          return section.PageSetup;
      }
      return null;
    }

    /// <summary>
    /// Gets a PageSetup object with default values for all properties.
    /// </summary>
    internal static PageSetup DefaultPageSetup
    {
      get
      {
        if (defaultPageSetup == null)
        {
          defaultPageSetup = new PageSetup();
		  defaultPageSetup.PageFormat = PdfSharp.Core.Enums.PageFormat.A4;
          defaultPageSetup.SectionStart = BreakType.BreakNextPage;
          defaultPageSetup.Orientation = PdfSharp.Core.Enums.Orientation.Portrait;
          defaultPageSetup.PageWidth = Unit.FromCentimeter(21);
          defaultPageSetup.PageHeight = Unit.FromCentimeter(29.7);
          defaultPageSetup.TopMargin = Unit.FromCentimeter(2.5);
          defaultPageSetup.BottomMargin = Unit.FromCentimeter(2);
          defaultPageSetup.LeftMargin = Unit.FromCentimeter(2.5);
          defaultPageSetup.RightMargin = Unit.FromCentimeter(2.5);
          defaultPageSetup.HeaderDistance =  Unit.FromCentimeter(1.25);
          defaultPageSetup.FooterDistance =  Unit.FromCentimeter(1.25);
          defaultPageSetup.OddAndEvenPagesHeaderFooter = false;
          defaultPageSetup.DifferentFirstPageHeaderFooter = false;
          defaultPageSetup.MirrorMargins = false;
          defaultPageSetup.HorizontalPageBreak = false;
#if DEBUG
          defaultPageSetupClone = defaultPageSetup.Clone();
#endif
        }
#if DEBUG
        else
        {
          Debug.Assert(PageSetup.defaultPageSetup.PageFormat == PageSetup.defaultPageSetupClone.PageFormat, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.SectionStart == PageSetup.defaultPageSetupClone.SectionStart, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.Orientation == PageSetup.defaultPageSetupClone.Orientation, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.PageWidth == PageSetup.defaultPageSetupClone.PageWidth, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.PageHeight == PageSetup.defaultPageSetupClone.PageHeight, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.TopMargin == PageSetup.defaultPageSetupClone.TopMargin, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.BottomMargin == PageSetup.defaultPageSetupClone.BottomMargin, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.LeftMargin == PageSetup.defaultPageSetupClone.LeftMargin, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.RightMargin == PageSetup.defaultPageSetupClone.RightMargin, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.HeaderDistance == PageSetup.defaultPageSetupClone.HeaderDistance, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.FooterDistance == PageSetup.defaultPageSetupClone.FooterDistance, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.OddAndEvenPagesHeaderFooter == PageSetup.defaultPageSetupClone.OddAndEvenPagesHeaderFooter, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.DifferentFirstPageHeaderFooter == PageSetup.defaultPageSetupClone.DifferentFirstPageHeaderFooter, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.MirrorMargins == PageSetup.defaultPageSetupClone.MirrorMargins, "DefaultPageSetup must not be modified");
          Debug.Assert(PageSetup.defaultPageSetup.HorizontalPageBreak == PageSetup.defaultPageSetupClone.HorizontalPageBreak, "DefaultPageSetup must not be modified");
        }
#endif
        return defaultPageSetup;
      }
    }
    private static PageSetup defaultPageSetup;
#if DEBUG
    private static PageSetup defaultPageSetupClone;
#endif

    #region Internal

	  
    #endregion
  }
}
