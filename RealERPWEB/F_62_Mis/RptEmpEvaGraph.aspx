<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpEvaGraph.aspx.cs" Inherits="RealERPWEB.F_62_Mis.RptEmpEvaGraph" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../js/bootstrap.js"></script>
    <script src="../Scripts/ScrollableGridPlugin.js"></script>
    <script src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">


                                <div class="form-group">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblNameSm" Text="Month:"></asp:Label>

                                        <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True"
                                            TabIndex="11" CssClass="ddlPage125 inputTxt">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Employee Name:"></asp:Label>



                                        <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgSearchSalesTeam" runat="server" OnClick="imgSearchSalesTeam_Click" Visible="false" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                    </div>



                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlEmpid" runat="server"  CssClass="form-control"
                                            TabIndex="5">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>


                                    </div>





                                </div>

                            </div>
                        </fieldset>

                        <div class="form-group">
                            <div class="col-md-6">
                               <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Height="270px" BorderColor="Black" Visible="false">
                                <asp:Chart ID="Chart1" runat="server" Height="264px" Width="500px">
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100" Interval="1">
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Titles>
                                        <asp:Title Font="Cambria, 16px" Name="Title1"
                                            Text="Monthly Sales Target Vs. Achievement">
                                        </asp:Title>
                                    </Titles>
                                </asp:Chart>
                                   </asp:Panel>
                            </div>

                            <div class="col-md-6">
                                <asp:Panel ID="Panel2" runat="server" BorderWidth="1px" Height="270px" BorderColor="Black" Visible="false">
                                <asp:Chart ID="Chart2" runat="server" Height="264px" Width="500px">
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100">
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Titles>
                                        <asp:Title Font="Cambria, 16px" Name="Title1"
                                            Text="Monthly Collection Target Vs. Achievement">
                                        </asp:Title>
                                    </Titles>
                                </asp:Chart>
                                    </asp:Panel>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-6">
                                <asp:Panel ID="Panel3" runat="server" BorderWidth="1px" Height="270px" BorderColor="Black" Visible="false">
                                <asp:Chart ID="Chart3" runat="server" Height="264px" Width="500px" >
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100">
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Titles>
                                        <asp:Title Font="Cambria, 16px" Name="Title1"
                                            Text="Monthly Call Target Vs. Achievement">
                                        </asp:Title>
                                    </Titles>
                                </asp:Chart>
                                    </asp:Panel>
                            </div>

                            <div class="col-md-6">
                                <asp:Panel ID="Panel4" runat="server" BorderWidth="1px" Height="270px" BorderColor="Black" Visible="false">
                                <asp:Chart ID="Chart4" runat="server" Height="264px" Width="500px" >
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100">
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Titles>
                                        <asp:Title Font="Cambria, 16px" Name="Title1"
                                            Text="Monthly Prospect Meeting Ext Target Vs. Achievement">
                                        </asp:Title>
                                    </Titles>
                                </asp:Chart>
                                    </asp:Panel>
                            </div>

                        </div>

                         <div class="form-group">
                            <div class="col-md-6">
                               <asp:Panel ID="Panel5" runat="server" BorderWidth="1px" Height="270px" BorderColor="Black" Visible="false">
                                <asp:Chart ID="Chart5" runat="server" Height="264px" Width="500px">
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100">
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Titles>
                                        <asp:Title Font="Cambria, 16px" Name="Title1"
                                            Text="Monthly Prospect Meeting Int Target Vs. Achievement">
                                        </asp:Title>
                                    </Titles>
                                </asp:Chart>
                                   </asp:Panel>
                            </div>

                            <div class="col-md-6">
                                <asp:Panel ID="Panel6" runat="server" BorderWidth="1px" Height="270px" BorderColor="Black" Visible="false">
                                <asp:Chart ID="Chart6" runat="server" Height="264px" Width="500px">
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100">
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Titles>
                                        <asp:Title Font="Cambria, 16px" Name="Title1"
                                            Text="Monthly Project Visit Target Vs. Achievement">
                                        </asp:Title>
                                    </Titles>
                                </asp:Chart>
                                    </asp:Panel>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-6">
                                <asp:Panel ID="Panel7" runat="server" BorderWidth="1px" Height="270px" BorderColor="Black" Visible="false">
                                <asp:Chart ID="Chart7" runat="server" Height="264px" Width="500px" >
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100">
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Titles>
                                        <asp:Title Font="Cambria, 16px" Name="Title1"
                                            Text="Monthly Offer Target Vs. Achievement">
                                        </asp:Title>
                                    </Titles>
                                </asp:Chart>
                                    </asp:Panel>
                            </div>

                            <div class="col-md-6">
                                <asp:Panel ID="Panel8" runat="server" BorderWidth="1px" Height="270px" BorderColor="Black" Visible="false">
                                <asp:Chart ID="Chart8" runat="server" Height="264px" Width="500px" >
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100">
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Titles>
                                        <asp:Title Font="Cambria, 16px" Name="Title1"
                                            Text="Monthly Others Target Vs. Achievement">
                                        </asp:Title>
                                    </Titles>
                                </asp:Chart>
                                    </asp:Panel>
                            </div>

                        </div>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


