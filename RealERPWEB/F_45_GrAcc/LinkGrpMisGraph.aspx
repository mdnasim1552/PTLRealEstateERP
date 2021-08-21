
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpMisGraph.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpMisGraph" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <cc1:BarChart ID="BarChart1" runat="server" CategoriesAxis="1,2,3" ChartTitle="Amount in Lac"
                            ChartHeight="350" ChartType="Column" ChartWidth="600">
                        </cc1:BarChart>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

