<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PrintChequeStatus.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.PrintChequeStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

           // $('.chzn-select').chosen({ search_contains: true });
        }

    </script>

    <style>
        .chkBoxControl label{
            margin:0;
        }
        .chzn-container{
            width: 100% !important;
            height:34px;
        }
    </style>
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
           <div class="card card-fluid mt-4">
                <div class="card-header">
                    <div class="row">
                       <%-- <div class="col-sm-12 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="form-label">From</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2_txtfromdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="form-label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1_txttodate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>--%>
                        <div class="col-sm-12 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="form-label">Cheque No.

                                <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="form-label" OnClick="ibtnFindProject_Click"><i class="fas fa-search "></i></asp:LinkButton>                                

                                </asp:Label>

                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                </div>
                            </div>

                        <div class="col-sm-12 col-md-3">
                            <div class="form-group" style="margin-top:20px;">   
                                
                                <asp:DropDownList ID="ddlChequeno" runat="server" CssClass="form-control form-control-sm chzn-select"  Width="336px" AutoPostBack="true" TabIndex="1">
                                </asp:DropDownList>

                              

                               
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-1">
                            <div class="form-group" style="margin-top:20px;">
                              
                                <asp:LinkButton ID="lbShow0" runat="server" CssClass="btn btn-primary btn-xs" Height="30px" OnClick="lblShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        
                    </div>
                </div>







                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>

                                        <asp:Label ID="lblCheckno" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                                <asp:LinkButton ID="lbtnChqUpdate" runat="server" Font-Bold="True" CssClass="btn btn-sm btn-danger" OnClick="lbtnChqUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque Status">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chqprint"  runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: center; background-color: Transparent"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqstatus"))=="True" %>'
                                            Width="70px"></asp:CheckBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    


                                </asp:TemplateField>
                           
                                                            
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>





                        <asp:Label ID="lblrecdate" runat="server" Visible="false"></asp:Label>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>