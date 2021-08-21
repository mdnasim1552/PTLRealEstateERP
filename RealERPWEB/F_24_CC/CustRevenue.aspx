<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CustRevenue.aspx.cs" Inherits="RealERPWEB.F_24_CC.CustRevenue" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
            $('.chzn-select').chosen({ search_contains: true });

        }
    </script>
    <style>
        .grvHeader th {
            font-weight: normal;
            text-align: center;
            text-transform: capitalize;
        }
        /*.cald {
             z-index: 1;
        }*/
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblMaterial" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" Visible="false" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select inputTxt" Width="350px" TabIndex="12">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>
                                        <asp:Label ID="lblProjectmDesc" runat="server" Visible="False" Width="350px" CssClass="lblTxt lblName txtAlgLeft"></asp:Label>


                                    </div>

                                    <div class=" col-md-7  pading5px">

                                        <asp:Label ID="lblSearch" CssClass=" smLbl_to" runat="server" Text="Unit Name"></asp:Label>
                                        <asp:TextBox ID="txtsrchunit" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnsrchunit" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnsrchunit_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        <asp:Label ID="lmsg" runat="server" CssClass="lblTxt lblName  btn-danger pull-right" Visible="false"></asp:Label>


                                    </div>



                                </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="831px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                         
                      >
                            <RowStyle />
                            <Columns>
                                <asp:CommandField ShowEditButton="True" HeaderStyle-Width="80px" CancelText="&lt;span class='glyphicon glyphicon-remove pull-left'&gt;&lt;/span&gt;" DeleteText="&lt;span class='glyphicon glyphicon-remove'&gt;&lt;/span&gt;" EditText="&lt;span class='glyphicon glyphicon-pencil'&gt;&lt;/span&gt;" UpdateText="&lt;span class='glyphicon glyphicon-ok'&gt;&lt;/span&gt;" />

                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hypcustomer" runat="server" Width="150px" Style=" color:black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'></asp:HyperLink>

                                        <%-- <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                            Width="150px"></asp:Label>--%>
                                    </ItemTemplate>



                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel2" runat="server">

                                            <asp:DropDownList ID="ddlClientName" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlClientName_SelectedIndexChanged" CssClass="chzn-select form-control inputTxt" TabIndex="12">
                                            </asp:DropDownList>
                                        <asp:TextBox ID="txtCustname" Visible="false" runat="server" Width="250px"  CssClass="form-control inputtextbox"></asp:TextBox>

                                        </asp:Panel>
                                    </EditItemTemplate>
                                    
                                    <HeaderStyle HorizontalAlign="Center" Width="250px" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size">

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>


                                        <asp:LinkButton ID="lbtnusize" runat="server" CommandArgument="lbtnusize"
                                            OnClick="lbtnusize_Click" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>


                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Parking">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminbmoneyaa" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Utility">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminbmvoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Co-oparative">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmincbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminsbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Min Booking Money" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minbam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Mgt Booking" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMgtBook" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgtbook1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <%--<asp:TemplateField HeaderText="Client Name" Visible="false">
                                    <EditItemTemplate>
                                        
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgcclientname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>



                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    <asp:LinkButton ID="lbtnBack" runat="server" OnClick="lbtnBack_Click" Visible="false" CssClass="btn btn-danger primaryBtn pull-right">Back</asp:LinkButton>

                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        
                        <asp:View ID="ViewPersonal" runat="server">
                            <fieldset class="scheduler-border fieldset_B" runat="server" visible="false">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lperInfo" runat="server" CssClass="btn btn-success primaryBtn" Text="Personal Information"></asp:Label>
                                        <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>
                                        <%--<asp:LinkButton ID="lbtnBack" runat="server"
                                            OnClick="lbtnBack_Click" CssClass="btn btn-danger primaryBtn pull-right">Back</asp:LinkButton>--%>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="form-group">
                                <asp:Label ID="lperInfo0" runat="server" CssClass="btn btn-success primaryBtn" Text="Revenue Information"></asp:Label>
                                <asp:Label ID="lblAcAmt" runat="server" Visible="False"></asp:Label>
                                <div class="clearfix"></div>

                            </div>
                            <div class="row">
                                <div class="col-md-7">

                                    <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvGcod" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalCost" runat="server" Font-Bold="True"
                                                        Font-Size="12px"  CssClass="btn btn-primary btn-xs" OnClick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">
                                                  <FooterTemplate>
                                                    <asp:LinkButton ID="lFinalUpdateCost" runat="server" CssClass="btn  btn-danger  btn-xs" OnClick="lFinalUpdateCost_Click"> Update  </asp:LinkButton>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lgdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="180px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgUnitnum" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                                        Width="50px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                         
                                          
                                            <asp:TemplateField HeaderText="Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvuamt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
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

                         




                         

                        </asp:View>


                      
                    </asp:MultiView>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
 
</asp:Content>



