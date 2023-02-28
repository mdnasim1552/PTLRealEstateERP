<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptFixAssetStatus02.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.RptFixAssetStatus02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style>
        .grvHeader th {
            font-weight: normal;
            text-align: center;
            text-transform: capitalize;
        }      
        .mt20{
            margin-top:20px;
        }
        .chzn-single {
            border-radius: 3px !important;
            height: 32px !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });     
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

            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row ">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblEmployee" runat="server" CssClass=" lblName lblTxt" Text="Employee List"></asp:Label>
                                <asp:TextBox ID="txtSrchEmployeeorRes" runat="server" CssClass="form-control form-control-sm" Width="100px"></asp:TextBox>                               
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                 <asp:LinkButton ID="ImgbtnFindEmp" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="ImgbtnFindEmp_Click"><i class="fas fa-search"></i></asp:LinkButton>
                             </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblreslist" runat="server" CssClass=" lblName lblTxt" Text="Resource List"></asp:Label>
                                <asp:DropDownList ID="ddlEmporResList" runat="server" AutoPostBack="True"  CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>                               
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                  <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                             </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid mb-1 ">
                <div class="card-body" style="min-height:520px">
                    <div class="row ">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="Viewequipst" runat="server">
                                <asp:GridView ID="gvrecandref" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-striped  table-bordered grvContentarea"
                                    OnRowDataBound="gvrecandref_RowDataBound" ShowFooter="True">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                    Style="text-align: Center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecDesc" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefDesc" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refunddesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refunddat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "refundqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>

                            </asp:View>
                            <asp:View ID="ViewRecIssaRefund" runat="server">
                                <asp:GridView ID="gvrecissaref" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-responsive table-bordered grvContentarea"
                                    OnRowDataBound="gvrecissaref_RowDataBound" ShowFooter="True" >

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecDescres" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecdateres" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecqtyres" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissueDescres" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuedesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissuedateres" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuedate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissueqtyres" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefDescres" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refunddesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefdateres" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refunddat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefqtyres" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "refundqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Store Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbalqtyres" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:View>  
                            <asp:View ID="ViewSum" runat="server">
                                <asp:GridView ID="gvrecissarefsum" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-responsive table-bordered grvContentarea"
                                    ShowFooter="True" >

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecDescsum" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Received">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecqtysum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issued">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissueqtyres0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Refund">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefqtysum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "refundqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Store Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbalqtysum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>        

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

