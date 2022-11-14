<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BillingMBEntry.aspx.cs" Inherits="RealERPWEB.F_09_PImp.BillingMBEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .grvContentarea {
            margin-right: 0px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });



        };



        function loadModalDetails() {
            $('#ModalAddMBInfo').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        };
        function CloseModalDetails() {
            $('#ModalAddMBInfo').modal('hide');
        };
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="card card-fluid mb-2">
                <div class="card-body">

                    <div class="row">
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblOrderDate" runat="server" class="control-label" Text="MB Date"></asp:Label>
                                <asp:TextBox ID="txtCurOrderDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurOrderDate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd.MM.yyyy" TargetControlID="txtCurOrderDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblOrderNo" runat="server" class="control-label" Text="MB No"></asp:Label>
                                <asp:Label ID="lblCurOrderNo1" runat="server" class="control-label" Text="POR00- "></asp:Label>
                                <asp:TextBox ID="txtCurOrderNo2" runat="server" CssClass="form-control form-control-sm" Text="00000" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblRefNo" runat="server" class="control-label" Text="Ref. No"></asp:Label>
                                <asp:TextBox ID="txtOrderRefNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;"></asp:LinkButton>
                        </div>

                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <label id="lblpreviousmb" runat="server">Previous MB</label>
                            <asp:LinkButton ID="lbtnPrevOrderList" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevOrderList_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            <asp:DropDownList ID="ddlPrevOrderList" runat="server" Width="180px" CssClass="form-control chzn-select">
                            </asp:DropDownList>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-sm-2 col-md-2 col-lg-">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" class="control-label" Text="Project"></asp:Label>
                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" class="control-label" Text="Contracttor"></asp:Label>

                                <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>


                    </div>



                    <asp:GridView ID="gvcorder" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" ShowFooter="True" Width="599px" CssClass="table-striped  table-bordered grvContentarea"
                        OnPageIndexChanging="gvcorder_PageIndexChanging" PageSize="15">
                        <PagerSettings Mode="NumericFirstLast" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Item Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblitemcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Floor Desc.">

                                <ItemTemplate>
                                    <asp:Label ID="lblFloordes" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>' Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Spec">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtspec" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spec")) %>' Width="100px" BorderStyle="None" TextMode="MultiLine"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Description">

                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                        OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblwrkdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                        Width="200px"></asp:Label>
                                    <asp:TextBox ID="txtwrkdesc" BackColor="Transparent" BorderStyle="None" runat="server" ReadOnly="true" TextMode="MultiLine" Height="50px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sdetails")).Trim()
                                         
                                                                         
                                                                    %>'
                                        Width="500px">
                                            <%--sdetails--%>
                                    </asp:TextBox>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvQty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvrate" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvAmount" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                   

                                    <asp:LinkButton ID="lbtnDetails" OnClick="lbtnDetails_Click" ToolTip="Add Details"  runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-history" aria-hidden="true"></span> </asp:LinkButton>


                                </ItemTemplate>
                                <ItemStyle Width="150px" />
                                <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                            </asp:TemplateField>




                        </Columns>

                        <HeaderStyle CssClass="grvHeaderNew" />
                        <PagerStyle CssClass="gvPaginationNew" />
                        <RowStyle CssClass="grvRowsNew" />
                        <FooterStyle CssClass="grvFooterNew" />

                    </asp:GridView>





                </div>
            </div>


            <div id="ModalAddMBInfo" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fas fa-info-circle"></i>&nbsp;Add Details</h5>
                            <asp:Label ID="lblmobile" runat="server"></asp:Label>
                            <button type="button" class="btn btn-xs btn-danger float-right" data-dismiss="modal" title="Close"><i class="fas fa-times-circle"></i></button>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row mb-1">
                               
                                <asp:GridView ID="gvdetails" runat="server" AutoGenerateColumns="False"
                            CssClass=" table-striped  table-bordered grvContentarea"
                            ShowFooter="True" Style="margin-right: 0px" Width="430px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Flrcode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdesc" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) 
                                                                        
                                                                         
                                                                    %>'
                                            Width="170px">
                                                                            
                                                                            
                                        </asp:Label>
                                    </ItemTemplate>
                                  
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnTotal" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" OnClick="lnkbtnTotal_Click"
                                            Style="text-decaration: none;">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvunit" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="NOS">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvnos" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nos"))  %>'
                                            Width="120px"></asp:TextBox>
                                    </ItemTemplate>
                                    
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Length">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvlength" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lnght")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Breadth">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "breadth")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Height">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvweight" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "height")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFWeight" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                     <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Unit Weight ">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvweight" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uweight")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFWeight" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Weight ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotalweight" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tweight")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoWeight" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                
                               
                               
                              
                            </Columns>

                            <HeaderStyle CssClass="grvHeaderNew" />
                        <PagerStyle CssClass="gvPaginationNew" />
                        <RowStyle CssClass="grvRowsNew" />
                        <FooterStyle CssClass="grvFooterNew" />

                        </asp:GridView>

                            </div>
                           
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnUpdatembinfo" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalDetails();" OnClick="lbtnUpdatembinfo_Click" ToolTip="Update Code Info.">
                                <i class="fas fa-save"></i>&nbsp;Update </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
