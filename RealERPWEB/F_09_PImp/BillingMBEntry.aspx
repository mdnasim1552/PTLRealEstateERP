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


            var gvdetails = $('#<%=this.gvdetails.ClientID %>');
            $.keynavigation(gvdetails);





        };



        function loadModalDetails() {
            $('#ModalAddMBInfo').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        };


        function OpenModalDetails() {
            $('#ModalAddMBInfo').modal('show', {
                backdrop: 'static',
                keyboard: false
            });

          
        };

        function CloseModalDetailsBack() {
            $('#ModalAddMBInfo').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            OpenModalDetails();

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
                                <asp:Label ID="lblmbno" runat="server" class="control-label" Text="MB No"></asp:Label>
                                <asp:Label ID="lblmbno1" runat="server" class="control-label" Text="MBK00- "></asp:Label>
                                <asp:TextBox ID="txtmbno2" runat="server" CssClass="form-control form-control-sm" Text="00000" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblRefNo" runat="server" class="control-label" Text="Ref. No"></asp:Label>
                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
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

                               
                                <ItemTemplate>
                                    <asp:Label ID="lblwrkdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                        Width="500px"></asp:Label>
                                   
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

                            <asp:TemplateField HeaderText="Order Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvQty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="Upto Bill">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvmbqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptombqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Balance Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvbalqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                            </asp:TemplateField>


<%--                            <asp:TemplateField HeaderText="Rate" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvrate" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
                          <%--  <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvAmount" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                            </asp:TemplateField>--%>



                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>


                                    <asp:LinkButton ID="lbtnDetails" OnClick="lbtnDetails_Click" ToolTip="Add Details" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-history" aria-hidden="true"></span> </asp:LinkButton>


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




                    <div>
                        <asp:HiddenField ID="hdnflrcod"  runat="server"/>
                        <asp:HiddenField ID="hdnorderno"  runat="server"/>
                        <asp:HiddenField ID="hdnrsircode"  runat="server"/>
                    </div>





                </div>
            </div>


            <div id="ModalAddMBInfo" class="modal fadeIn " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-xl">
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
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                   
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Center" />
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
                                                <asp:Label ID="lblgvflrcod" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Sl" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvserial" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sl")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                              <ItemStyle HorizontalAlign="Center" />
                                              
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdesc" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) 
                                                                        
                                                                         
                                                                    %>'
                                                    Width="250px">
                                                                            
                                                                            
                                                </asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                             <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnCalculation" runat="server" Font-Bold="True"
                                                    OnClick="lnkbtnCalculation_Click"
                                                    CssClass="btn btn-sm btn-warning">Calculation</asp:LinkButton>
                                            </FooterTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvunit" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                              <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnTotal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" OnClick="lnkbtnTotal_Click"
                                                    CssClass="btn btn-sm btn-primary">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="NOS">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvnos" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nos")).ToString("#,##0.00;-#,##0.00; ")  %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>


                                           

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Length">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvlength" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
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
                                                <asp:TextBox ID="txtgvbreadth" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
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
                                                <asp:TextBox ID="txtgvheight" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"  Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "height")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFheight" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Unit Weight ">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvuweight" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uweight")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                           
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Weight ">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvtotalweight" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tweight")).ToString("#,##0.0000;-#,##0.0000; ")  %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoWeight" runat="server" Font-Bold="True" 
                                                   Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Remarks ">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvremarks" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                    Width="250px"></asp:TextBox>
                                            </ItemTemplate>
                                           
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                          <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                   
                                                    <asp:LinkButton ID="lbtnAddRow" OnClick="lbtnAddRow_Click"   runat="server" CssClass="btn btn-default btn-xs"><span  class="fa  fa-plus" aria-hidden="true"></span> </asp:LinkButton>


                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                         <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                   
                                                    <asp:LinkButton ID="lbtnDeldet" OnClick="lbtnDeldet_Click"   runat="server"  CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle" aria-hidden="true"></span> </asp:LinkButton>


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
