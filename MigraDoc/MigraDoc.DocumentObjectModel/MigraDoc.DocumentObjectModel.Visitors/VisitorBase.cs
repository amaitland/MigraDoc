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

using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Shapes.Charts;

namespace MigraDoc.DocumentObjectModel.Visitors
{
  /// <summary>
  /// Summary description for VisitorBase.
  /// </summary>
  public abstract class VisitorBase : DocumentObjectVisitor
  {
    public VisitorBase()
    {
    }

    public override void Visit(DocumentObject documentObject)
    {
      IVisitable visitable = documentObject as IVisitable;
      if (visitable != null)
        visitable.AcceptVisitor(this, true);
    }

    protected void FlattenParagraphFormat(ParagraphFormat format, ParagraphFormat refFormat)
    {
      if (format.alignment == null)
        format.alignment = refFormat.alignment;

      if (format.firstLineIndent == null)
        format.firstLineIndent = refFormat.firstLineIndent;

      if (format.leftIndent == null)
        format.leftIndent = refFormat.leftIndent;

      if (format.rightIndent == null)
        format.rightIndent = refFormat.rightIndent;

      if (format.spaceBefore == null)
        format.spaceBefore = refFormat.spaceBefore;

      if (format.spaceAfter == null)
        format.spaceAfter = refFormat.spaceAfter;

      if (format.lineSpacingRule == null)
        format.lineSpacingRule = refFormat.lineSpacingRule;
      if (format.lineSpacing == null)
        format.lineSpacing = refFormat.lineSpacing;

      if (format.widowControl == null)
        format.widowControl = refFormat.widowControl;

      if (format.keepTogether == null)
        format.keepTogether = refFormat.keepTogether;

      if (format.keepWithNext == null)
        format.keepWithNext = refFormat.keepWithNext;

      if (format.pageBreakBefore == null)
        format.pageBreakBefore = refFormat.pageBreakBefore;

      if (format.outlineLevel == null)
        format.outlineLevel = refFormat.outlineLevel;

      if (format.font == null)
      {
        if (refFormat.font != null)
        {
          //The font is cloned here to avoid parent problems
          format.font = refFormat.font.Clone();
          format.font.parent = format;
        }
      }
      else if (refFormat.font != null)
        FlattenFont(format.font, refFormat.font);

      if (format.shading == null)
      {
        if (refFormat.shading != null)
        {
          format.shading = refFormat.shading.Clone();
          format.shading.parent = format;
        }
        //        format.shading = refFormat.shading;
      }
      else if (refFormat.shading != null)
        FlattenShading(format.shading, refFormat.shading);

      if (format.borders == null)
        format.borders = refFormat.borders;
      else if (refFormat.borders != null)
        FlattenBorders(format.borders, refFormat.borders);

      //      if (format.tabStops == null)
      //        format.tabStops = refFormat.tabStops;
      if (refFormat.tabStops != null)
        FlattenTabStops(format.TabStops, refFormat.tabStops);

      if (refFormat.listInfo != null)
        FlattenListInfo(format.ListInfo, refFormat.listInfo);
    }

    protected void FlattenListInfo(ListInfo listInfo, ListInfo refListInfo)
    {
      if (listInfo.continuePreviousList == null)
        listInfo.continuePreviousList = refListInfo.continuePreviousList;
      if (listInfo.listType == null)
        listInfo.listType = refListInfo.listType;
      if (listInfo.numberPosition == null)
        listInfo.numberPosition = refListInfo.numberPosition;
    }

    protected void FlattenFont(Font font, Font refFont)
    {
      if (font.name == null)
        font.name = refFont.name;
      if (font.size == null)
        font.size = refFont.size;
      if (font.color == null)
        font.color = refFont.color;
      if (font.underline == null)
        font.underline = refFont.underline;
      if (font.bold == null)
        font.bold = refFont.bold;
      if (font.italic == null)
        font.italic = refFont.italic;
      if (font.superscript == null)
        font.superscript = refFont.superscript;
      if (font.subscript == null)
        font.subscript = refFont.subscript;
    }

    protected void FlattenShading(Shading shading, Shading refShading)
    {
      //fClear?
      if (shading.visible == null)
        shading.visible = refShading.visible;
      if (shading.color == null)
        shading.color = refShading.color;
    }

    protected Border FlattenedBorderFromBorders(Border border, Borders parentBorders)
    {
      if (border == null)
        border = new Border(parentBorders);

      if (border.visible == null)
        border.visible = parentBorders.visible;

      if (border.style == null)
        border.style = parentBorders.style;

      if (border.width == null)
        border.width = parentBorders.width;

      if (border.color == null)
        border.color = parentBorders.color;

      return border;
    }

    protected void FlattenBorders(Borders borders, Borders refBorders)
    {
      if (borders.visible == null)
        borders.visible = refBorders.visible;
      if (borders.width == null)
        borders.width = refBorders.width;
      if (borders.style == null)
        borders.style = refBorders.style;
      if (borders.color == null)
        borders.color = refBorders.color;

      if (borders.distanceFromBottom == null)
        borders.distanceFromBottom = refBorders.distanceFromBottom;
      if (borders.distanceFromRight == null)
        borders.distanceFromRight = refBorders.distanceFromRight;
      if (borders.distanceFromLeft == null)
        borders.distanceFromLeft = refBorders.distanceFromLeft;
      if (borders.distanceFromTop == null)
        borders.distanceFromTop = refBorders.distanceFromTop;

      if (refBorders.left != null)
      {
        FlattenBorder(borders.Left, refBorders.left);
        FlattenedBorderFromBorders(borders.left, borders);
      }
      if (refBorders.right != null)
      {
        FlattenBorder(borders.Right, refBorders.right);
        FlattenedBorderFromBorders(borders.right, borders);
      }
      if (refBorders.top != null)
      {
        FlattenBorder(borders.Top, refBorders.top);
        FlattenedBorderFromBorders(borders.top, borders);
      }
      if (refBorders.bottom != null)
      {
        FlattenBorder(borders.Bottom, refBorders.bottom);
        FlattenedBorderFromBorders(borders.bottom, borders);
      }
    }

    protected void FlattenBorder(Border border, Border refBorder)
    {
      if (border.visible == null)
        border.visible = refBorder.visible;
      if (border.width == null)
        border.width = refBorder.width;
      if (border.style == null)
        border.style = refBorder.style;
      if (border.color == null)
        border.color = refBorder.color;
    }

    protected void FlattenTabStops(TabStops tabStops, TabStops refTabStops)
    {
      if (!tabStops.fClearAll)
      {
        foreach (TabStop refTabStop in refTabStops)
        {
          if (tabStops.GetTabStopAt(refTabStop.Position) == null && refTabStop.AddTab)
            tabStops.AddTabStop(refTabStop.Position, refTabStop.Alignment, refTabStop.Leader);
        }
      }

      for (int i = 0; i < tabStops.Count; i++)
      {
        TabStop tabStop = tabStops[i];
        if (!tabStop.AddTab)
          tabStops.RemoveObjectAt(i);
      }
      //Die TabStopCollection ist so wie sie jetzt ist vollst�ndig.
      //Sie darf daher nichts erben, d.h. :
      tabStops.fClearAll = true;
    }

    protected void FlattenPageSetup(PageSetup pageSetup, PageSetup refPageSetup)
    {
      Unit dummyUnit;
      if (pageSetup.pageWidth == null && pageSetup.pageHeight == null)
      {
        if (pageSetup.pageFormat == null)
        {
          pageSetup.pageWidth = refPageSetup.pageWidth;
          pageSetup.pageHeight = refPageSetup.pageHeight;
          pageSetup.pageFormat = refPageSetup.pageFormat;
        }
        else
          PageSetup.GetPageSize(pageSetup.PageFormat, out pageSetup.pageWidth, out pageSetup.pageHeight);
      }
      else
      {
        if (pageSetup.pageWidth == null)
        {
          if (pageSetup.pageFormat == null)
            pageSetup.pageHeight = refPageSetup.pageHeight;
          else
            PageSetup.GetPageSize(pageSetup.PageFormat, out dummyUnit, out pageSetup.pageHeight);
        }
        else if (pageSetup.pageHeight == null)
        {
          if (pageSetup.pageFormat == null)
            pageSetup.pageWidth = refPageSetup.pageWidth;
          else
            PageSetup.GetPageSize(pageSetup.PageFormat, out pageSetup.pageWidth, out dummyUnit);
        }
      }
      //      if (pageSetup.pageWidth == null)
      //        pageSetup.pageWidth = refPageSetup.pageWidth;
      //      if (pageSetup.pageHeight == null)
      //        pageSetup.pageHeight = refPageSetup.pageHeight;
      //      if (pageSetup.pageFormat == null)
      //        pageSetup.pageFormat = refPageSetup.pageFormat;
      if (pageSetup.sectionStart == null)
        pageSetup.sectionStart = refPageSetup.sectionStart;
      if (pageSetup.orientation == null)
        pageSetup.orientation = refPageSetup.orientation;
      if (pageSetup.topMargin == null)
        pageSetup.topMargin = refPageSetup.topMargin;
      if (pageSetup.bottomMargin == null)
        pageSetup.bottomMargin = refPageSetup.bottomMargin;
      if (pageSetup.leftMargin == null)
        pageSetup.leftMargin = refPageSetup.leftMargin;
      if (pageSetup.rightMargin == null)
        pageSetup.rightMargin = refPageSetup.rightMargin;
      if (pageSetup.headerDistance == null)
        pageSetup.headerDistance = refPageSetup.headerDistance;
      if (pageSetup.footerDistance == null)
        pageSetup.footerDistance = refPageSetup.footerDistance;
      if (pageSetup.oddAndEvenPagesHeaderFooter == null)
        pageSetup.oddAndEvenPagesHeaderFooter = refPageSetup.oddAndEvenPagesHeaderFooter;
      if (pageSetup.differentFirstPageHeaderFooter == null)
        pageSetup.differentFirstPageHeaderFooter = refPageSetup.differentFirstPageHeaderFooter;
      if (pageSetup.mirrorMargins == null)
        pageSetup.mirrorMargins = refPageSetup.mirrorMargins;
      if (pageSetup.horizontalPageBreak == null)
        pageSetup.horizontalPageBreak = refPageSetup.horizontalPageBreak;
    }

    protected void FlattenHeaderFooter(HeaderFooter headerFooter, bool isHeader)
    {
    }

    protected void FlattenFillFormat(FillFormat fillFormat)
    {
    }

    protected void FlattenLineFormat(LineFormat lineFormat, LineFormat refLineFormat)
    {
      if (refLineFormat != null)
      {
        if (lineFormat.width == null)
          lineFormat.width = refLineFormat.width;
      }
    }

    protected void FlattenAxis(Axis axis)
    {
      if (axis == null)
        return;

      LineFormat refLineFormat = new LineFormat();
      refLineFormat.width = 0.15;
      if (axis.hasMajorGridlines.Value && axis.majorGridlines != null)
        FlattenLineFormat(axis.majorGridlines.lineFormat, refLineFormat);
      if (axis.hasMinorGridlines.Value && axis.minorGridlines != null)
        FlattenLineFormat(axis.minorGridlines.lineFormat, refLineFormat);

      refLineFormat.width = 0.4;
      if (axis.lineFormat != null)
        FlattenLineFormat(axis.lineFormat, refLineFormat);

      //      axis.majorTick;
      //      axis.majorTickMark;
      //      axis.minorTick;
      //      axis.minorTickMark;

      //      axis.maximumScale;
      //      axis.minimumScale;

      //      axis.tickLabels;
      //      axis.title;
    }

    protected void FlattenPlotArea(PlotArea plotArea)
    {
    }

    protected void FlattenDataLabel(DataLabel dataLabel)
    {
    }


    #region Chart
    internal override void VisitChart(Chart chart)
    {
      Document document = chart.Document;
      if (chart.style == null)
        chart.style = Style.DefaultParagraphName;
      Style style = document.Styles[chart.style];
      if (chart.format == null)
      {
        chart.format = style.paragraphFormat.Clone();
        chart.format.parent = chart;
      }
      else
        FlattenParagraphFormat(chart.format, style.paragraphFormat);


      FlattenLineFormat(chart.lineFormat, null);
      FlattenFillFormat(chart.fillFormat);

      FlattenAxis(chart.xAxis);
      FlattenAxis(chart.yAxis);
      FlattenAxis(chart.zAxis);

      FlattenPlotArea(chart.plotArea);

      //      if (this.hasDataLabel.Value)
      FlattenDataLabel(chart.dataLabel);

    }
    #endregion

    #region Document
    internal override void VisitDocument(Document document)
    {
    }

    internal override void VisitDocumentElements(DocumentElements elements)
    {
    }
    #endregion

    #region Format
    internal override void VisitStyle(Style style)
    {
      Style baseStyle = style.GetBaseStyle();
      if (baseStyle != null && baseStyle.paragraphFormat != null)
      {
        if (style.paragraphFormat == null)
          style.paragraphFormat = baseStyle.paragraphFormat;
        else
          FlattenParagraphFormat(style.paragraphFormat, baseStyle.paragraphFormat);
      }
    }

    internal override void VisitStyles(Styles styles)
    {
    }
    #endregion

    #region Paragraph
    internal override void VisitFootnote(Footnote footnote)
    {
      Document document = footnote.Document;

      ParagraphFormat format = null;

      Style style = document.styles[footnote.style];
      if (style != null)
        format = ParagraphFormatFromStyle(style);
      else
      {
        footnote.Style = "Footnote";
        format = document.styles[footnote.Style].paragraphFormat;
      }

      if (footnote.format == null)
      {
        footnote.format = format.Clone();
        footnote.format.parent = footnote;
      }
      else
        FlattenParagraphFormat(footnote.format, format);

    }

    internal override void VisitParagraph(Paragraph paragraph)
    {
      Document document = paragraph.Document;

      ParagraphFormat format = null;

      DocumentObject currentElementHolder = GetDocumentElementHolder(paragraph);
      Style style = document.styles[paragraph.style];
      if (style != null)
        format = ParagraphFormatFromStyle(style);

      else if (currentElementHolder is Cell)
      {
        paragraph.style = ((Cell)currentElementHolder).style;
        format = ((Cell)currentElementHolder).format;
      }
      else if (currentElementHolder is HeaderFooter)
      {
        HeaderFooter currHeaderFooter = ((HeaderFooter)currentElementHolder);
        if (currHeaderFooter.IsHeader)
        {
          paragraph.Style = "Header";
          format = document.styles["Header"].paragraphFormat;
        }
        else
        {
          paragraph.Style = "Footer";
          format = document.styles["Footer"].paragraphFormat;
        }

        if (currHeaderFooter.format != null)
          FlattenParagraphFormat(paragraph.Format, currHeaderFooter.format);
      }
      else if (currentElementHolder is Footnote)
      {
        paragraph.Style = "Footnote";
        format = document.styles["Footnote"].paragraphFormat;
      }
      else if (currentElementHolder is TextArea)
      {
        paragraph.style = ((TextArea)currentElementHolder).style;
        format = ((TextArea)currentElementHolder).format;
      }
      else
      {
        if (paragraph.style != "")
          paragraph.Style = "InvalidStyleName";
        else
          paragraph.Style = "Normal";
        format = document.styles[paragraph.Style].paragraphFormat;
      }

      if (paragraph.format == null)
      {
        paragraph.format = format.Clone();
        paragraph.format.parent = paragraph;
      }
      else
        FlattenParagraphFormat(paragraph.format, format);
    }
    #endregion

    #region Section
    internal override void VisitHeaderFooter(HeaderFooter headerFooter)
    {
      Document document = headerFooter.Document;
      string styleString;
      if (headerFooter.IsHeader)
        styleString = "Header";
      else
        styleString = "Footer";

      ParagraphFormat format;
      Style style = document.styles[headerFooter.style];
      if (style != null)
        format = ParagraphFormatFromStyle(style);
      else
      {
        format = document.styles[styleString].paragraphFormat;
        headerFooter.Style = styleString;
      }

      if (headerFooter.format == null)
      {
        headerFooter.format = format.Clone();
        headerFooter.format.parent = headerFooter;
      }
      else
        FlattenParagraphFormat(headerFooter.format, format);
    }

    internal override void VisitHeadersFooters(HeadersFooters headersFooters)
    {
    }

    internal override void VisitSection(Section section)
    {
      Section prevSec = section.PreviousSection();
      PageSetup prevPageSetup = PageSetup.DefaultPageSetup;
      if (prevSec != null)
      {
        prevPageSetup = prevSec.pageSetup;

        if (!section.Headers.HasHeaderFooter(HeaderFooterIndex.Primary))
          section.Headers.primary = prevSec.Headers.primary;
        if (!section.Headers.HasHeaderFooter(HeaderFooterIndex.EvenPage))
          section.Headers.evenPage = prevSec.Headers.evenPage;
        if (!section.Headers.HasHeaderFooter(HeaderFooterIndex.FirstPage))
          section.Headers.firstPage = prevSec.Headers.firstPage;

        if (!section.Footers.HasHeaderFooter(HeaderFooterIndex.Primary))
          section.Footers.primary = prevSec.Footers.primary;
        if (!section.Footers.HasHeaderFooter(HeaderFooterIndex.EvenPage))
          section.Footers.evenPage = prevSec.Footers.evenPage;
        if (!section.Footers.HasHeaderFooter(HeaderFooterIndex.FirstPage))
          section.Footers.firstPage = prevSec.Footers.firstPage;
      }

      if (section.pageSetup == null)
        section.pageSetup = prevPageSetup;
      else
        FlattenPageSetup(section.pageSetup, prevPageSetup);
    }

    internal override void VisitSections(Sections sections)
    {
    }
    #endregion

    #region Shape
    internal override void VisitTextFrame(TextFrame textFrame)
    {
      if (textFrame.height == null)
        textFrame.height = Unit.FromInch(1);
      if (textFrame.width == null)
        textFrame.width = Unit.FromInch(1);
    }
    #endregion

    #region Table
    internal override void VisitCell(Cell cell)
    {
      // format, shading and borders are already processed.
    }

    internal override void VisitColumns(Columns columns)
    {
      foreach (Column col in columns)
      {
        if (col.width == null)
          col.width = columns.width;

        if (col.width == null)
          col.width = "2.5cm";
      }
    }

    internal override void VisitRow(Row row)
    {
      foreach (Cell cell in row.Cells)
      {
        if (cell.verticalAlignment == null)
          cell.verticalAlignment = row.verticalAlignment;
      }
    }

    internal override void VisitRows(Rows rows)
    {
      foreach (Row row in rows)
      {
        if (row.height == null)
          row.height = rows.height;
        if (row.heightRule == null)
          row.heightRule = rows.heightRule;
        if (row.verticalAlignment == null)
          row.verticalAlignment = rows.verticalAlignment;
      }
    }
    /// <summary>
    /// Returns a paragraph format object initialized by the given style.
    /// It differs from style.ParagraphFormat if style is a character style.
    /// </summary>
    ParagraphFormat ParagraphFormatFromStyle(Style style)
    {
      if (style.Type == StyleType.Character)
      {
        Document doc = style.Document;
        ParagraphFormat format = style.paragraphFormat.Clone();
        FlattenParagraphFormat(format, doc.Styles.Normal.ParagraphFormat);
        return format;
      }
      else
        return style.paragraphFormat;
    }

    internal override void VisitTable(Table table)
    {
      Document document = table.Document;

      if (table.leftPadding == null)
        table.leftPadding = Unit.FromMillimeter(1.2);
      if (table.rightPadding == null)
        table.rightPadding = Unit.FromMillimeter(1.2);

      ParagraphFormat format;
      Style style = document.styles[table.style];
      if (style != null)
        format = ParagraphFormatFromStyle(style);
      else
      {
        table.Style = "Normal";
        format = document.styles.Normal.paragraphFormat;
      }

      if (table.format == null)
      {
        table.format = format.Clone();
        table.format.parent = table;
      }
      else
        FlattenParagraphFormat(table.format, format);

      int rows = table.Rows.Count;
      int clms = table.Columns.Count;

      for (int idxclm = 0; idxclm < clms; idxclm++)
      {
        Column column = table.Columns[idxclm];
        ParagraphFormat colFormat;
        style = document.styles[column.style];
        if (style != null)
          colFormat = ParagraphFormatFromStyle(style);
        else
        {
          column.style = table.style;
          colFormat = table.Format;
        }

        if (column.format == null)
        {
          column.format = colFormat.Clone();
          column.format.parent = column;
          if (column.format.shading == null && table.format.shading != null)
            column.format.shading = table.format.shading;
        }
        else
          FlattenParagraphFormat(column.format, colFormat);

        if (column.leftPadding == null)
          column.leftPadding = table.leftPadding;
        if (column.rightPadding == null)
          column.rightPadding = table.rightPadding;

        if (column.shading == null)
          column.shading = table.shading;

        else if (table.shading != null)
          FlattenShading(column.shading, table.shading);

        if (column.borders == null)
          column.borders = table.borders;
        else if (table.borders != null)
          FlattenBorders(column.borders, table.borders);
      }

      for (int idxrow = 0; idxrow < rows; idxrow++)
      {
        Row row = table.Rows[idxrow];

        ParagraphFormat rowFormat;
        style = document.styles[row.style];
        if (style != null)
        {
          rowFormat = ParagraphFormatFromStyle(style);
        }
        else
        {
          row.style = table.style;
          rowFormat = table.Format;
        }

        for (int idxclm = 0; idxclm < clms; idxclm++)
        {
          Column column = table.Columns[idxclm];
          Cell cell = row[idxclm];

          ParagraphFormat cellFormat;
          Style cellStyle = document.styles[cell.style];
          if (cellStyle != null)
          {
            cellFormat = ParagraphFormatFromStyle(cellStyle);

            if (cell.format == null)
              cell.format = cellFormat;
            else
              FlattenParagraphFormat(cell.format, cellFormat);
          }
          else
          {
            if (row.format != null)
              FlattenParagraphFormat(cell.Format, row.format);

            if (style != null)
            {
              cell.style = row.style;
              FlattenParagraphFormat(cell.Format, rowFormat);
            }
            else
            {
              cell.style = column.style;
              FlattenParagraphFormat(cell.Format, column.format);
            }
          }

          if (cell.format.shading == null && table.format.shading != null)
            cell.format.shading = table.format.shading;

          if (cell.shading == null)
            cell.shading = row.shading;
          else if (row.shading != null)
            FlattenShading(cell.shading, row.shading);
          if (cell.shading == null)
            cell.shading = column.shading;
          else if (column.shading != null)
            FlattenShading(cell.shading, column.shading);
          if (cell.borders == null)
            cell.borders = row.borders;
          else if (row.borders != null)
            FlattenBorders(cell.borders, row.borders);
          if (cell.borders == null)
            cell.borders = column.borders;
          else if (column.borders != null)
            FlattenBorders(cell.borders, column.borders);
        }

        if (row.format == null)
        {
          row.format = rowFormat.Clone();
          row.format.parent = row;
          if (row.format.shading == null && table.format.shading != null)
            row.format.shading = table.format.shading;
        }
        else
          FlattenParagraphFormat(row.format, rowFormat);

        if (row.topPadding == null)
          row.topPadding = table.topPadding;
        if (row.bottomPadding == null)
          row.bottomPadding = table.bottomPadding;

        if (row.shading == null)
          row.shading = table.shading;
        else if (table.shading != null)
          FlattenShading(row.shading, table.shading);

        if (row.borders == null)
          row.borders = table.borders;
        else if (table.borders != null)
          FlattenBorders(row.borders, table.borders);
      }
    }
    #endregion


    internal override void VisitLegend(Legend legend)
    {
      ParagraphFormat parentFormat;
      if (legend.style != null)
      {
        Style style = legend.Document.Styles[legend.Style];
        if (style == null)
          style = legend.Document.Styles["InvalidStyleName"];

        parentFormat = style.paragraphFormat;
      }
      else
      {
        TextArea textArea = (TextArea)GetDocumentElementHolder(legend);
        legend.style = textArea.style;
        parentFormat = textArea.format;
      }
      if (legend.format == null)
        legend.Format = parentFormat.Clone();
      else
        FlattenParagraphFormat(legend.format, parentFormat);
    }

    internal override void VisitTextArea(TextArea textArea)
    {
      if (textArea == null || textArea.elements == null)
        return;

      Document document = textArea.Document;

      ParagraphFormat parentFormat;

      if (textArea.style != null)
      {
        Style style = textArea.Document.Styles[textArea.Style];
        if (style == null)
          style = textArea.Document.Styles["InvalidStyleName"];

        parentFormat = style.paragraphFormat;
      }
      else
      {
        Chart chart = (Chart)textArea.parent;
        parentFormat = chart.format;
        textArea.style = chart.style;
      }

      if (textArea.format == null)
        textArea.Format = parentFormat.Clone();
      else
        FlattenParagraphFormat(textArea.format, parentFormat);

      FlattenFillFormat(textArea.fillFormat);
      FlattenLineFormat(textArea.lineFormat, null);
    }


    private DocumentObject GetDocumentElementHolder(DocumentObject docObj)
    {
      DocumentElements docEls = (DocumentElements)docObj.parent;
      return docEls.parent;
    }
  }
}
