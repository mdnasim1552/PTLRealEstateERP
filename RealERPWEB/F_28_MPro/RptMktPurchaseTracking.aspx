<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMktPurchaseTracking.aspx.cs" Inherits="RealERPWEB.F_28_MPro.RptMktPurchaseTracking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        };

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid mt-3">
                <div class="card-body" style="min-height: 450px;">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="Purchasetrk" runat="server">
                            <asp:Label ID="lblproject" runat="server" Style="text-align: center !important" Font-Bold="true" Font-Size="16px" ForeColor="DeepPink">
                            </asp:Label>
                            <div class="table-responsive">
                                <asp:GridView ID="gvPurstk01" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass=" table-responsive  table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppDat0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />                                           
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Chalan No ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvchalano" Style="word-break: break-all" runat="server" Width="70px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlno"))  %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                           
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pur. Req. Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPrType" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prtypedesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvActType" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttypedesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />                                           
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Marketing Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMktType" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mkttypedesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppRate01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>                                           
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSupplier01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>                                         
                                        </asp:TemplateField>

                                    </Columns>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                    <FooterStyle CssClass="grvFooterNew" />
                                </asp:GridView>

                            </div>
                            <asp:Panel ID="pnlnarration" runat="server" Visible="false">
                                <div class="row mb-2 mt-2">
                                    <div class="col-1">
                                        <asp:Label ID="lblReqNarr" runat="server" Font-Bold="true" Text="Narration:"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:TextBox ID="txtReqNarr" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:HyperLink ID="lnkCreateMat" runat="server" CssClass="btn btn-warning primaryBtn" Visible="false"
                                        NavigateUrl="~/F_17_Acc/AccSubCodeBook.aspx?InputType=Res" Target="_blank">Create Material</asp:HyperLink>
                                </div>
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="transferview" runat="server">
                            <asp:Panel ID="pnlA" runat="server">
                                <asp:Label ID="lblheader" runat="server" Style="text-align: center !important" Font-Bold="true" Font-Size="12px" ForeColor="blue">
                                </asp:Label>
                            </asp:Panel>
                            <asp:Panel ID="pnlB" runat="server" Visible="false">
                                <fieldset class="scheduler-border fieldset_B">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Transfer Requisition"></asp:Label>
                                                <asp:TextBox ID="txtSrcRequisition01" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="imgbtnFindReqno01" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindReqno01_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:DropDownList ID="ddltreq" runat="server" CssClass="chzn-select form-control inputTxt" Style="width: 336px;">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 pading5px  asitCol1">
                                                <asp:LinkButton ID="lbtnOk0" runat="server" CssClass="btn btn-primary primaryBtn"
                                                    OnClick="lbtnOk0_Click">Ok</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </asp:Panel>

                            <div class="table-responsive">
                                <asp:GridView ID="gvtreqtrk" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass=" table-responsive  table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3A" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGenNoA" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppDat0A" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrdttex")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgrefnoA" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials3A" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit2A" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "runit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpecificationA" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreqty01A" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppRate01A" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamountA" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusernameA" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusernameB" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postdttext")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry Terminal">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusernameC" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postrmid")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>
                            <asp:Panel ID="pnltranNar" runat="server" Visible="false">
                                <fieldset class="scheduler-border fieldset_D">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <div class="input-group">
                                                    <span class="input-group-addon glypingraddon">
                                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                    </span>
                                                    <asp:TextBox ID="txtNartrans" runat="server" class="form-control" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-4 pading5px">
                                                <div class="input-group">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-warning primaryBtn" Visible="false"
                                                        NavigateUrl="~/F_17_Acc/AccSubCodeBook.aspx?InputType=Res" Target="_blank">Create Material</asp:HyperLink>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </fieldset>
                            </asp:Panel>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

