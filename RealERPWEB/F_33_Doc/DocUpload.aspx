<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DocUpload.aspx.cs" Inherits="RealERPWEB.F_33_Doc.DocUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <script type="text/javascript">


        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            try {
              
                $('.chzn-select').chosen({ search_contains: true });
                
            }
            catch(e) {
                alert(e);
            }
         
       };

        </script> 
    
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <fieldset class="scheduler-border fieldset_A">

                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-5 pading5px asitCol5">
                                    <asp:Label ID="Label20" runat="server" Font-Bold="True" CssClass="smLbl_to lblTxt" Font-Size="12px"
                                        Text="Type:" Width="110px"></asp:Label>


                                    <asp:TextBox ID="txtType" CssClass="inputtextbox" runat="server" Font-Bold="True"
                                        Width="65px"></asp:TextBox>

                                    <asp:ImageButton ID="imgbtnFindType" runat="server" Height="17px"
                                        ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnFindType_Click"
                                        Width="16px" />

                                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" CssClass="inputTxt"
                                        Font-Bold="True" Font-Size="12px" Width="200px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4 pading5px asitCol4">
                                    <asp:Label ID="Label1" runat="server" CssClass="smLbl_to lblTxt" Font-Size="12px" Text="ID No.:" Width="110px"></asp:Label>

                                    <asp:Label ID="lblCurMSRNo1" runat="server" CssClass="smLbl_to lblTxt" Font-Bold="True" Font-Size="12px" Text="Id No-0001" Width="60px"></asp:Label>



                                    <asp:Label ID="Label13" runat="server"  style="margin-left: 10px;"
                                        Font-Size="12px" Text="Date:" Width="30px" CssClass="lblDate"></asp:Label>

                                    <asp:TextBox ID="txtDate" runat="server" Font-Size="12px" ToolTip="(dd-MMM-yyyy)"
                                        Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                                    <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True"></asp:Label>

                                </div>
                                <div />
                                <div />
                    </fieldset>

                    <fieldset class="scheduler-border fieldset_A">

                        <div class="form-horizontal">

                            <div class="form-group">
                                <div class="col-md-4 pading5px asitCol4">
                                    <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12px"
                                         Text="Short Name:" Width="90px"></asp:Label>

                                    <asp:TextBox ID="txtsName" runat="server" Font-Size="12px"
                                        Width="200px"></asp:TextBox>


                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="12px" style="float:left;margin-top: 20px;"
                                       Text="Details:" Width="90px"></asp:Label>

                                    <asp:TextBox ID="txtDetails1" runat="server" TextMode="MultiLine" style="margin-top: 10px;"
                                        Width="200px"></asp:TextBox>


                                </div>
                                
                                  <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="12px"
                                        Text="Company Documents:"
                                        Width="120px"></asp:Label>

                                    <asp:Image ID="EmpImg" runat="server" Height="100px" Width="100px" />
                                    <asp:FileUpload ID="imgFileUpload" runat="server" Height="26px"
                                        onchange="submitform();" Width="216px" />

                                    <asp:LinkButton ID="lbtnUpdateImg" CssClass="btn btn-success" runat="server" OnClick="lbtnUpdateImg_Click"
                                       >Update</asp:LinkButton>
                                </div>
                            </div>

                            <div />
                    </fieldset>
                    <div />
                    <div />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



