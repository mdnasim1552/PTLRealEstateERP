﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktBookigApp2.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktBookigApp2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        $(document).ready(function () {


            $('.chzn-select').chosen({ search_contains: true });

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });

        }

        function openModal() {
            $('#imgNuploadModal').modal('toggle');
        }

        function closeModal() {
            $('#imgNuploadModal').modal('hide');
        }

    </script>

    <style>
        #ContentPlaceHolder1_GridViewPriceDetail > tr:nth-of-type(3) {
            display: none;
        }
         


        
        .switch {
          position: relative;
          display: inline-block;
          width: 60px;
          height: 34px;
        }

        .switch input { 
          opacity: 0;
          width: 0;
          height: 0;
        }

        .slider {
          position: absolute;
          cursor: pointer;
          top: 0;
          left: 0;
          right: 0;
          bottom: 0;
          background-color: #ccc;
          -webkit-transition: .4s;
          transition: .4s;
        }

        .slider:before {
          position: absolute;
          content: "";
          height: 26px;
          width: 26px;
          left: 4px;
          bottom: 4px;
          background-color: white;
          -webkit-transition: .4s;
          transition: .4s;
        }

        input:checked + .slider {
          background-color: #2196F3;
        }

        input:focus + .slider {
          box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
          -webkit-transform: translateX(26px);
          -ms-transform: translateX(26px);
          transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
          border-radius: 34px;
        }

        .slider.round:before {
          border-radius: 50%;
        }


        /*#ContentPlaceHolder1_ddlProjectName_chzn {
            width: 300px;
        }

        #ContentPlaceHolder1_ddlProjectName_chzn .chzn-drop {
            width: 300px!important;
        }  */      
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
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

                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSrcProject" runat="server" TabIndex="3" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindProject" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="4" CssClass="btn btn-sm">Project Name</asp:LinkButton>
                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" TabIndex="5">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSrcCustomer" runat="server" TabIndex="3" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindCustomer" runat="server" OnClick="imgbtnFindCustomer_Click" TabIndex="4" CssClass="btn btn-sm">Unit Name</asp:LinkButton>
                                        <asp:DropDownList ID="ddlCustName" runat="server" CssClass="form-control form-control-sm chzn-select"
                                            TabIndex="5">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 10px;">
                                        <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="Customer Number"></asp:Label><br>
                                        <asp:Label ID="lblCustmerNumber" runat="server" ></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-1" style="margin-top: 30px;">
                                    <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-sm btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                </div>

                                <div class="col-md-1" style="margin-top: 26px;">
                                    <label class="switch">
                                      <%--<input type="checkbox" id="saleDeclaration">
                                      <span class="slider round"></span>--%>

                                       <asp:CheckBox ID="saleDeclaration" runat="server"  />
                                        <span class="slider round"></span>
                                    </label>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height:400px;">
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="ViewRegistration" runat="server">
                                    <div style="background-color: darkcyan; font-weight: bold; color: white;">
                                        <asp:Label ID="lblBuilidinginfo" runat="server" CssClass=" inputlblVal inputlblvalstyle" Style="width: 300px;" Text="Building Information"></asp:Label>
                                    </div>
                                    <br>
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lbldate" runat="server" CssClass="control-label" Text="Application Date"></asp:Label>
                                                    <asp:TextBox ID="txtdate" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>

                                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender1" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div class="col-md-12">
                                        <div class="col-md-8">
                                            <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" Width="720px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                                <RowStyle Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0"
                                                                runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle
                                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                ID="lblgvItmCode" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle
                                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">

                                                        <ItemTemplate>
                                                            <asp:Label
                                                                ID="lgcResDesc1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True"
                                                            HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center"
                                                            VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                ID="lgvgval" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>

                                                        <ItemTemplate>
                                                            <asp:TextBox
                                                                ID="txtgvVal" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="300px"></asp:TextBox>
                                                            <asp:TextBox
                                                                ID="txtgvdVal" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="300px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender_txtgvdVal" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                            <asp:CheckBoxList ID="cbldesc" runat="server" CellPadding="2" CellSpacing="0"
                                                                CssClass=" rbtnList1 chkBoxControl" Font-Bold="True" Font-Size="14px"
                                                                ForeColor="#000" Height="12px" RepeatColumns="6" Width="300px"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="Apartment">Apartment</asp:ListItem>
                                                                <asp:ListItem Value="Shop">Shop</asp:ListItem>
                                                                <asp:ListItem Value="Office">Office</asp:ListItem>
                                                                <asp:ListItem Value="Others">Others</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </ItemTemplate>
                                                        <HeaderStyle
                                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-8">
                                            <div style="background-color: darkcyan; font-weight: bold; color: white;">
                                                <asp:Label ID="Label2" runat="server" CssClass=" inputlblVal inputlblvalstyle" Style="width: 300px;" Text="Price Details"></asp:Label>
                                            </div>
                                            <asp:GridView ID="GridViewPriceDetail" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" Width="720px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                                <RowStyle Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNoper"
                                                                runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle
                                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                ID="lblgvItmCodeper" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "code")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle
                                                            HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">

                                                        <ItemTemplate>
                                                            <asp:Label
                                                                ID="lgNominatedDescription" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "description")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="Calculation"
                                                                runat="server"
                                                                OnClick="llbtnCalculation_Click" CssClass="btn btn-sm  btn-primary  primarygrdBtn">Total</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True"
                                                            HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center"
                                                            VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox
                                                                ID="txtgvValAmount" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "amount")) %>'
                                                                Width="150px" Style="text-align: right;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div style="background-color: darkcyan; font-weight: bold; color: white;">
                                        <asp:Label ID="lblpayinfo" runat="server" CssClass=" inputlblVal inputlblvalstyle" Style="width: 300px;" Text="Payment Information"></asp:Label>
                                    </div>
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <%--<div class="col-md-2">
                                                            <div class"form-group">
                                                                <asp:Label ID="lblbookamt" runat="server" CssClass="control-label" Text="Payment Amount"></asp:Label>
                                                                <asp:TextBox ID="txtbookamt" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </div>
                                                        </div>--%>

                                                        


                                                        <div class="col-md-3">
                                                            <div class"form-group">
                                                                <asp:Label ID="Lblrcvbookingam" runat="server" CssClass="control-label" Text="Received Amount During Booking"></asp:Label>
                                                                <asp:TextBox ID="txtrcvbookingam" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <div class"form-group">
                                                               <asp:Label ID="lblCheqNo" runat="server" CssClass="control-label" Text="Cash/Cheque/P.O/D.D. No"></asp:Label>
                                                                <asp:TextBox ID="txtCheqNo" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class"form-group">
                                                               <asp:Label ID="lblbankname" runat="server" CssClass="control-label" Text="Bank Name"></asp:Label>
                                                                <asp:TextBox ID="txtbankname" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    
                                                        <div class="col-md-2">
                                                            <div class"form-group">
                                                               <asp:Label ID="lblbankbranch" runat="server" CssClass="control-label" Text="Branch"></asp:Label>
                                                                <asp:TextBox ID="txtbankbranch" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <div class"form-group">
                                                               <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="Install Amnt Per Month"></asp:Label>
                                                                <asp:TextBox ID="Textinsamt" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <div class"form-group">
                                                                <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="No of Total Installment"></asp:Label>
                                                                <asp:TextBox ID="TxtNoTInstall" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-2">
                                                            <div class"form-group">
                                                               <asp:Label ID="lblInstallmentDate" runat="server" CssClass="control-label" Text="Installment Date"></asp:Label>
                                                                <asp:TextBox ID="txtInstallmentDate" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <div class"form-group">
                                                               <asp:Label ID="lblbookdate" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                                                               <asp:TextBox ID="txtbookdate" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender_txtbookdate" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtbookdate" CssClass="form-control form-control-sm"></cc1:CalendarExtender>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <div class"form-group">
                                                               <asp:Label ID="lblBookAmt" runat="server" CssClass="control-label" Text="Booking Amount"></asp:Label>
                                                                <asp:TextBox ID="TextBookingAmt" runat="server" TabIndex="5" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </div>
                                                        </div>


                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    <div style="background-color: darkcyan; font-weight: bold; color: white;">
                                        <asp:Label ID="lblInstype" runat="server" CssClass=" inputlblVal inputlblvalstyle" Style="width: 300px;" Text="Installment Info"></asp:Label>
                                    </div>
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-12 pading5px">
                                                    <asp:Label ID="lblintavailloan" runat="server" CssClass="lblTxt lblName" Style="width: 130px;" Text="Interested to avail loan:"></asp:Label>
                                                    <asp:CheckBoxList ID="cblintavailloan" runat="server" CellPadding="2" CellSpacing="0"
                                                        CssClass=" rbtnList1 chkBoxControl" Font-Bold="True" Font-Size="14px"
                                                        ForeColor="#000" Height="12px" RepeatColumns="6" Width="200px"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No" Selected="True">No</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-12 pading5px">
                                                        <asp:Label ID="lblpaytype" runat="server" CssClass="lblTxt lblName" Style="width: 130px;" Text="Mode of Payment:"></asp:Label>

                                                        <asp:CheckBoxList ID="cblpaytype" runat="server" CellPadding="2" CellSpacing="0"
                                                            CssClass=" rbtnList1 chkBoxControl" Font-Bold="True" Font-Size="14px"
                                                            ForeColor="#000" Height="12px" RepeatColumns="6" Width="200px"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="Installment">Installment</asp:ListItem>
                                                            <asp:ListItem Value="OneTime" Selected="True">One Time</asp:ListItem>

                                                        </asp:CheckBoxList>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div style="background-color: darkcyan; font-weight: bold; color: white;">
                                        <asp:Label ID="lblPersonalInfo" runat="server" CssClass=" inputlblVal inputlblvalstyle" Style="width: 300px;" Text="Personal Information"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <fieldset class="scheduler-border fieldset_A">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName text-warning">Upload Image</asp:Label>
                                                    <asp:Image ID="EmpImg" runat="server" Height="100px" Width="100px" />
                                                    <div>
                                                        <asp:FileUpload ID="imgFileUpload" runat="server" Height="26px"
                                                            ToolTip="Employee Image" onchange="submitform();" Width="216px" />
                                                        <asp:Button ID="btnUpload" CssClass="btn btn-success" runat="server" Text="Upload" OnClick="btnUpload_OnClick" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <asp:GridView ID="gvperinfo" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="720px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNoper"
                                                        runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle
                                                    HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lblgvItmCodeper" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle
                                                    HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">

                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lgcResDescper" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True"
                                                    HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center"
                                                    VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lgvgvalper" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>

                                                <ItemTemplate>
                                                    <asp:TextBox
                                                        ID="txtgvValper" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Width="300px"></asp:TextBox>
                                                    <asp:TextBox
                                                        ID="txtgvdValper" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Width="300px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender_txtgvdValper" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdValper"></cc1:CalendarExtender>

                                                    <asp:CheckBoxList ID="cbldescper" runat="server" CellPadding="2" CellSpacing="0"
                                                        CssClass=" rbtnList1 chkBoxControl" Font-Bold="True" Font-Size="14px"
                                                        ForeColor="#000" Height="12px" RepeatColumns="6" Width="300px"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Open">Open</asp:ListItem>
                                                        <asp:ListItem Value="Confidential">Confidential</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                    <asp:Image ID="Image1" runat="server" Height="50px" Width="50px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))=="01290" || Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))=="01291") ? true: false%>' ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' />
                                                    <asp:LinkButton ID="lnkbtnImg" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Nominee Image Upload.. " BackColor="Transparent" OnClick="lnkbtnImg_Click" Width="30px" Style="float: right"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                    <div style="background-color: darkcyan; font-weight: bold; color: white;">
                                        <asp:Label ID="lblNomineeDetails" runat="server" CssClass=" inputlblVal inputlblvalstyle" Style="width: 300px;" Text="Nominee Details"></asp:Label>
                                    </div>

                                    <div class="col-md-4">
                                        <fieldset class="scheduler-border fieldset_A">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <asp:Label ID="lblNomineeImg" runat="server" CssClass="lblTxt lblName text-warning">Upload Image</asp:Label>
                                                    <asp:Image ID="ImageNominee" runat="server" Height="100px" Width="100px" />
                                                    <div>
                                                        <asp:FileUpload ID="imgUploadNominee" runat="server" Height="26px"
                                                            ToolTip="Nominee Image" onchange="submitform();" Width="216px" />
                                                        <asp:Button ID="btnUploadNominee" CssClass="btn btn-success" runat="server" Text="Upload" OnClick="btnUploadNominee_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <asp:GridView ID="GridViewNominee" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="720px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNoper"
                                                        runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle
                                                    HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lblgvItmCodeper" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle
                                                    HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lgNomineeDescription" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True"
                                                    HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center"
                                                    VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lgvgvalNominee" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox
                                                        ID="txtValNominee" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Width="300px"></asp:TextBox>
                                                    <asp:TextBox
                                                        ID="txtgvdValNominee" runat="server" BackColor="Transparent" BorderStyle="None" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Width="300px"></asp:TextBox>

                                                    <cc1:CalendarExtender ID="CalendarExtender_txtgvdValNominee" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdValNominee"></cc1:CalendarExtender>
                                                    <asp:Panel runat="server" Visible="false">
                                                        <asp:CheckBoxList ID="cbldescper" runat="server" CellPadding="2" CellSpacing="0" Visible="false"
                                                            CssClass=" rbtnList1 chkBoxControl" Font-Bold="True" Font-Size="14px"
                                                            ForeColor="#000" Height="12px" RepeatColumns="6" Width="300px"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="Open">Open</asp:ListItem>
                                                            <asp:ListItem Value="Confidential">Confidential</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                        <asp:Image ID="Image1" runat="server" Height="50px" Width="50px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))=="01290" || Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))=="01291") ? true: false%>' ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' />
                                                        <asp:LinkButton ID="lnkbtnImg" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Nominee Image Upload.. " BackColor="Transparent" OnClick="lnkbtnImg_Click" Width="30px" Style="float: right"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                    <div style="background-color: darkcyan; font-weight: bold; color: white;">
                                        <asp:Label ID="lblNominated" runat="server" CssClass=" inputlblVal inputlblvalstyle" Style="width: 300px;" Text="Nominated Correspondents"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <fieldset class="scheduler-border fieldset_A">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName text-warning">Upload Image</asp:Label>
                                                    <asp:Image ID="ImageCorrespondent" runat="server" Height="100px" Width="100px" />
                                                    <div>
                                                        <asp:FileUpload ID="imgUploadCorrespondent" runat="server" Height="26px"
                                                            ToolTip="Correspondent Image" onchange="submitform();" Width="216px" />
                                                        <asp:Button ID="btnUploadCorrespondent" CssClass="btn btn-success" runat="server" Text="Upload" OnClick="btnUploadCorrespondent_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <asp:GridView ID="GridViewNominated" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="720px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNoper"
                                                        runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle
                                                    HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lblgvItmCodeper" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle
                                                    HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lgNominatedDescription" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True"
                                                    HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center"
                                                    VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lgvgvalNominated" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox
                                                        ID="txtgvValNominated" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Width="300px"></asp:TextBox>
                                                    <asp:TextBox
                                                        ID="txtgvdValNominated" runat="server" Visible="false" BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Width="300px"></asp:TextBox>

                                                    <cc1:CalendarExtender ID="CalendarExtender_txtgvdValNominated" runat="server" Enabled="True" Format="dd-MMM-yyyy" PopupPosition="TopLeft" PopupButtonID="txtgvdValNominated" TargetControlID="txtgvdValNominated"></cc1:CalendarExtender>
                                                    <asp:CheckBoxList ID="cbldescper" runat="server" CellPadding="2" CellSpacing="0" Visible="false"
                                                        CssClass=" rbtnList1 chkBoxControl" Font-Bold="True" Font-Size="14px"
                                                        ForeColor="#000" Height="12px" RepeatColumns="6" Width="300px"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Open">Open</asp:ListItem>
                                                        <asp:ListItem Value="Confidential">Confidential</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                    <asp:Panel runat="server" Visible="false">
                                                        <asp:Image ID="Image1" runat="server" Height="50px" Width="50px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))=="01290" || Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))=="01291") ? true: false%>' ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' />
                                                        <asp:LinkButton ID="lnkbtnImg" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Nominee Image Upload.. " BackColor="Transparent" OnClick="lnkbtnImg_Click" Width="30px" Style="float: right"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                                    </asp:Panel>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                    





                                    <div style="background-color: darkcyan; font-weight: bold; color: white;">
                                        <asp:Label ID="Label7" runat="server" CssClass=" inputlblVal inputlblvalstyle" Style="width: 300px;" Text="Remarks"></asp:Label>
                                    </div>

                                    <asp:GridView ID="GridViewRemarks" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="720px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNoper"
                                                        runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle
                                                    HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lblgvItmCodeper" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle
                                                    HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lgNominatedDescription" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True"
                                                    HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center"
                                                    VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        ID="lgvgvalRmrk" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox
                                                        ID="txtgvValRmrk" runat="server" BackColor="Transparent"  TextMode ="multiline"  BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Width="300px"></asp:TextBox>

                                                    <asp:TextBox
                                                        ID="txtgvdValNominated" runat="server" Visible="false" BackColor="Transparent" TextMode="multiline" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                        Width="300px"></asp:TextBox>


                                                    <asp:Panel runat="server" Visible="false">
                                                        <asp:Image ID="Image1" runat="server" Height="50px" Width="50px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))=="01290" || Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))=="01291") ? true: false%>' ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' />
                                                        <asp:LinkButton ID="lnkbtnImg" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Nominee Image Upload.. " BackColor="Transparent" OnClick="lnkbtnImg_Click" Width="30px" Style="float: right"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                                    </asp:Panel>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdatInfo" runat="server" OnClick="lUpdatInfo_Click" CssClass="btn btn-danger primarygrdBtn">Update Information</asp:LinkButton>
                                                </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                </div>
            </div>
            </div>

            <div class="modal fade" id="imgNuploadModal" tabindex="-1" role="dialog" aria-labelledby="imgNuploadModal" aria-hidden="true">
                <!-- .modal-dialog -->
                <div class="modal-dialog modal-dialog-scrollable" role="document">
                    <!-- .modal-content -->
                    <div class="modal-content">
                        <!-- .modal-header -->
                        <div class="modal-header">
                            <h6 id="imgNuploadModalLabel" class="modal-title"><span class="fa fa-user-tag"></span>Change Nominee Picture </h6>
                        </div>
                        <!-- /.modal-header -->
                        <!-- .modal-body -->
                        <div class="modal-body px-0">

                            <div class="card-body">
                                <div id="dropzone" class="fileinput-dropzone">
                                    <asp:Label ID="lblgcode" runat="server" Visible="false"></asp:Label>
                                    <span>Drop files or click to upload.</span>
                                    <!-- The file input field used as target for the file upload widget -->
                                    <asp:FileUpload ID="imgFileUploadN" runat="server" onchange="submitform();" />
                                </div>
                                <div id="progress" class="progress progress-xs rounded-0 fade">
                                    <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal-body -->
                        <!-- .modal-footer -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                        <!-- /.modal-footer -->
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

