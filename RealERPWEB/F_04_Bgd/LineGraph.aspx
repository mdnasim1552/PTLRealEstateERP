<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LineGraph.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.LineGraph" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.js" ></script>
<script language="javascript" type="text/javascript" src="../Scripts/jquery-1.7.1.min.js" ></script>
<script language="javascript" type="text/javascript" src="../Scripts/jquery.jqplot.min.js"></script>
<script language="javascript" type="text/javascript" src="../Scripts/jqplot.highlighter.min.js"></script>
<script language="javascript" type="text/javascript" src="../Scripts/jqplot.cursor.min.js"></script>
<script language="javascript" type="text/javascript" src="../Scripts/jqplot.dateAxisRenderer.min.js"></script>


<script  language="javascript" type="text/javascript">

    $(document).ready(function () {

        var line1 = [['23-May-08', 578.55], ['20-Jun-08', 566.5], ['25-Jul-08', 480.88], ['22-Aug-08', 509.84],
      ['26-Sep-08', 454.13], ['24-Oct-08', 379.75], ['21-Nov-08', 303], ['26-Dec-08', 308.56],
      ['23-Jan-09', 299.14], ['20-Feb-09', 346.51], ['20-Mar-09', 325.99], ['24-Apr-09', 386.15]];
        var plot1 = $.jqplot('chart1', [line1], {
            title: 'Data Point Highlighting',
            axes: {
                xaxis: {
                    renderer: $.jqplot.DateAxisRenderer,
                    tickOptions: {
                        formatString: '%b&nbsp;%#d'
                    }
                },
                yaxis: {
                    tickOptions: {
                        formatString: '$%.2f'
                    }
                }
            },
            highlighter: {
                show: true,
                sizeAdjust: 7.5
            },
            cursor: {
                show: false
            }
        });



       
    });


    function pageLoaded() {

        
    }



</script>
</asp:Content>


