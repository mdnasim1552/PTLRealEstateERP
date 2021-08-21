
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryGanttChart.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.EntryGanttChart" %>


<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div class="container moduleItemWrpper">
                        <div class="contentPart">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                          <asp:Panel ID="Panel2" runat="server">

                                              <div class="form-group">
                                                 <div class="col-md-11 pading5px asitCol11">
                                                    <asp:Label ID="lblProjectList" runat="server" CssClass=" lblName lblTxt" Text="Project Name:" ></asp:Label>

                                                 <asp:TextBox ID="txtProjectSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                                  <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    <asp:DropDownList ID="ddlProject" runat="server" Width="300" CssClass="ddlPage"   TabIndex="2"></asp:DropDownList>
                                                                                                
                                                 <asp:Label ID="lblProjectDesc" runat="server" CssClass=" inputtextbox" Width="300" Visible="false"></asp:Label>
                                                   
                                                   <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                                   <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                   <ProgressTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text="Please wait . . . . . . ." Width="120px" CssClass="lblName lblTxt"></asp:Label>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                               
                                                </div>
                                              </div>
                                            </asp:Panel>

                                        <asp:Panel ID="PnlColoumn" runat="server" Visible="false">
                                             <div class="form-group">
                                                 <div class="col-md-11 pading5px asitCol11">

                                                    <asp:Label ID="lbl01" runat="server" CssClass=" lblName lblTxt" Text="Start Date:" ></asp:Label>

                                                     <asp:Label ID="lblStartDate" runat="server" CssClass="inputtextbox" ></asp:Label>

                                                     <asp:Label ID="lbl2" runat="server" CssClass=" smLbl_to" Text="End Date:" ></asp:Label>

                                                     <asp:Label ID="lblEndDate" runat="server" CssClass=" inputtextbox" ></asp:Label>

                                                     <asp:Label ID="lbl3" runat="server" CssClass=" smLbl_to" Text="Duration:" ></asp:Label>

                                                    <asp:Label ID="lblDuration" runat="server" CssClass="inputtextbox" ></asp:Label>

                                                     <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Page Size:" Visible="False"  ></asp:Label>

                                                      <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"  CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                                            Visible="False" Width="60px">
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                        </asp:DropDownList>
                                              
                                                </div>
                                              </div>
                                        </asp:Panel>

                                         <asp:Panel ID="Panel1" runat="server">

                                              <div class="form-group">
                                                 <div class="col-md-11 pading5px asitCol11">
                                                   <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server" 
                                                        Width="100%"  DataStartField="startdate" 
                                                        DataEndField="enddate" DataTextField="isirdesc" DataIdField="id" DataResourceField="isircode"
                                                        CellGroupBy="Month" Scale="Week" EventMoveHandling="CallBack" 
                                                        OnEventMove="DayPilotScheduler1_EventMove" BorderStyle="None"  
                                                        ViewType="Gantt" style="top: 0px; left: 0px" DurationBarColor="#660066" >
                                                    </DayPilot:DayPilotScheduler>
                                               
                                                </div>
                                              </div>
                                         </asp:Panel>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                    </div>

                <%--<tr>
                    <td colspan="12">
                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="style17" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left;" Text="Project Name:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style104">
                                        <asp:TextBox ID="txtProjectSearch" runat="server" BorderStyle="None" Height="18px"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style103">
                                        <asp:ImageButton ID="ImgbtnFindProject" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ImgbtnFindProject_Click" TabIndex="1" Width="16px" />
                                    </td>
                                    <td class="style108">
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="newStyle1" Font-Bold="True"
                                            Font-Size="11px" Height="21px" TabIndex="2" Width="300px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectDesc" runat="server" BackColor="White" CssClass="newStyle1"
                                            Font-Bold="True" Font-Size="12px" ForeColor="Blue" Height="18px" Style="font-weight: 700"
                                            Visible="False" Width="300px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" CssClass="style15" Font-Bold="True" Font-Size="12px"
                                            OnClick="lbtnOk_Click" Style="text-align: center;" TabIndex="3" Width="50px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style111">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow" Style="text-align: left"
                                                    Text="Please wait . . . . . . ." Width="120px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                  
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>--%>
                <%--<asp:Panel ID="PnlColoumn" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="1px" Visible="False">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style111">
                                        <asp:Label ID="lbl01" runat="server" CssClass="style27" Font-Bold="True" Font-Size="12px"
                                            Font-Underline="False" ForeColor="White" Height="16px" Style="font-weight: 700;
                                            text-align: left" Text="Start Date:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style111">
                                        <asp:Label ID="lblStartDate" runat="server" CssClass="style27" Font-Bold="True" Font-Size="12px"
                                            Font-Underline="False" ForeColor="White" Height="16px" Style="text-align: left"
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td class="style110">
                                        <asp:Label ID="lbl2" runat="server" CssClass="style27" Font-Bold="True" Font-Size="12px"
                                            Font-Underline="False" ForeColor="White" Height="16px" Style="font-weight: 700;
                                            text-align: left" Text="End Date:" Width="65px"></asp:Label>
                                    </td>
                                    <td class="style109">
                                        <asp:Label ID="lblEndDate" runat="server" CssClass="style27" Font-Bold="True" Font-Size="12px"
                                            Font-Underline="False" ForeColor="White" Height="16px" Style="font-weight: 700;
                                            text-align: left" Width="70px"></asp:Label>
                                    </td>
                                    <td class="style113">
                                        <asp:Label ID="lbl3" runat="server" CssClass="style27" Font-Bold="True" Font-Size="12px"
                                            Font-Underline="False" ForeColor="White" Height="16px" Style="font-weight: 700;
                                            text-align: left" Text="Duration:" Width="60px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDuration" runat="server" CssClass="style27" Font-Bold="True" Font-Size="12px"
                                            Font-Underline="False" ForeColor="White" Height="16px" Style="font-weight: 700;
                                            text-align: left" Width="73px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Style="color: #FFFFFF;
                                            text-align: right;" Text="Page Size:" Visible="False" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style115">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Visible="False" Width="60px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style114">
                                        &nbsp;
                                    </td>
                                   
                                </tr>
                            </table>
                        </asp:Panel>--%>
                <%--<table style="width: 100%;">
                                <tr>
                                    <td colspan="12">
                                        <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server" 
                                            Width="100%"  DataStartField="startdate" 
                                            DataEndField="enddate" DataTextField="isirdesc" DataIdField="id" DataResourceField="isircode"
                                            CellGroupBy="Month" Scale="Week" EventMoveHandling="CallBack" 
                                            OnEventMove="DayPilotScheduler1_EventMove" BorderStyle="None"  
                                            ViewType="Gantt" style="top: 0px; left: 0px" DurationBarColor="#660066" >
                                        </DayPilot:DayPilotScheduler>
                                    </td>
                                    --%>
              
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
