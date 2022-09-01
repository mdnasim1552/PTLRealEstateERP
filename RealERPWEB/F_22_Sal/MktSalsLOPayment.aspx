<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MktSalsLOPayment.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktSalsLOPayment" %>

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



            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });
            $('.chosen-continer').css({ 'width': '200px', "height": "20px" });

            $('.chzn-select').chosen({ search_contains: true });

            GetcomDiscountVisibility();

        }



        function GetcomDiscountVisibility() {
            var comcod =<%=this.GetCompCode()%>;

            switch (comcod) {
                case 3353:
                    $('#<%=this.ldT.ClientID%>').hide();
                    $('#<%=this.ldiscountt.ClientID%>').hide();
                    $('#<%=this.ldiscountp.ClientID%>').hide();

                    break;



            }


        }

        function loadModalAddJob() {
            $('#AddJob').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }

        function CloseModalAddJob() {
            $('#AddJob').modal('hide');


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
                            OnRowCancelingEdit="gvSpayment_RowCancelingEdit" OnRowDataBound="gvSpayment_RowDataBound"
                            OnRowEditing="gvSpayment_RowEditing" OnRowUpdating="gvSpayment_RowUpdating">
                            <RowStyle />
                            <Columns>
                                <asp:CommandField ShowEditButton="True" HeaderStyle-Width="80px" CancelText="&lt;span class='glyphicon glyphicon-remove pull-left'&gt;&lt;/span&gt;" DeleteText="&lt;span class='glyphicon glyphicon-remove'&gt;&lt;/span&gt;" EditText="&lt;span class='glyphicon glyphicon-pencil'&gt;&lt;/span&gt;" UpdateText="&lt;span class='glyphicon glyphicon-ok'&gt;&lt;/span&gt;" />

                                <asp:TemplateField HeaderText="Sl">
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
                                        <asp:HyperLink ID="hypcustomer" runat="server" Width="150px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'></asp:HyperLink>

                                        <%-- <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                            Width="150px"></asp:Label>--%>
                                    </ItemTemplate>



                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel2" runat="server">

                                            <asp:DropDownList ID="ddlClientName" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlClientName_SelectedIndexChanged" CssClass="chzn-select form-control inputTxt" TabIndex="12">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtCustname" Visible="false" runat="server" Width="250px" placeholder="English Name" CssClass="form-control inputtextbox" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'></asp:TextBox>
                                            <asp:TextBox ID="txtCustnameBN" Visible="false" runat="server" Width="250px" placeholder="Bangla Name" CssClass="form-control inputtextbox"></asp:TextBox>

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





                                <asp:TemplateField HeaderText="Client Name BN">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgcclientnamegdatatbn" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custnamebn")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Scope of Work" Visible="false">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddldesign" CssClass="ddlPage chzn-select" runat="server" Width="100px">
                                        </asp:DropDownList>

                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdesign" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "designdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="File code" Visible="false">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgfilecode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fcode")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Customer Id">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgfilecode22" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fcode")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Car Parking">
                                    <EditItemTemplate></EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCarParking" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cparking")) %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


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
                                        <asp:Label ID="lblwork" runat="server" Visible="False" Width="63px"></asp:Label>
                                        <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>
                                        <asp:Label ID="lblvoucher" runat="server" Visible="False" Width="63px"></asp:Label>
                                        <%--<asp:LinkButton ID="lbtnBack" runat="server"
                                            OnClick="lbtnBack_Click" CssClass="btn btn-danger primaryBtn pull-right">Back</asp:LinkButton>--%>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </fieldset>
                            <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="831px" Visible="false">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                Width="49px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcResDesc1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="200px" ForeColor="Black" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgval" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <FooterTemplate>

                                            <%--<asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-warning primaryBtn" OnClick="lUpdatPerInfo_Click">Update Personal Info</asp:LinkButton>--%>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                Width="510px" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                Height="20px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>


                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            <div class="form-group">
                                <asp:Label ID="lperInfo0" runat="server" CssClass="btn btn-success primaryBtn" Text="Revenue Information"></asp:Label>
                                <asp:Label ID="lblAcAmt" runat="server" Visible="False"></asp:Label>
                                <div class="clearfix"></div>

                            </div>
                            <div class="row">
                                <div class="col-md-9">

                                    <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
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
                                                        Font-Size="12px" ForeColor="#000" OnClick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">

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
                                            <asp:TemplateField HeaderText="Unit Size">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvUSize" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lFinalUpdateCost" runat="server" CssClass="btn  btn-danger primaryBtn" OnClick="lFinalUpdateCost_Click"> Update  </asp:LinkButton>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:TextBox ID="lgvRate" runat="server" ForeColor="Black" BorderStyle="none" BackColor="Transparent"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="90px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Discount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDiscount" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvdiscount" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Total Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFttlAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvttluamt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="LO Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFloAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvlouamt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "louamt")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Company Amount">
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


                                            <asp:TemplateField HeaderText="Remarks" Visible="false">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks"))%>'
                                                        Width="100px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />

                                    </asp:GridView>
                                    <fieldset class="scheduler-border fieldset_B">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                                <asp:Label ID="ldT" runat="server" CssClass="lblTxt lblName" Width="50" Text="DISCOUNT"></asp:Label>

                                                <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                                <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                                <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                                <asp:Label ID="ldiscountp" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                                <asp:Label ID="ldiscountt" runat="server" CssClass="lblTxt lblName" Width="100px"></asp:Label>
                                                <asp:Label ID="ldiscounttprint" runat="server" CssClass="lblTxt lblName" Width="100px" Visible="false"></asp:Label>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                    </fieldset>

                                    <asp:Label ID="lblInword" runat="server" CssClass="lblTxt lblName" Style="width: 600px; color: blue; text-align: left;"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Panel ID="Panel2" runat="server">


                                            <div class="col-md-12 pading5px">

                                                <div class="form-group">

                                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Sales Team</asp:Label>


                                                    <asp:LinkButton ID="ibtnFindSalesteam" Visible="false" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindSalesteam_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                                    <asp:DropDownList ID="ddlSalesTeam" runat="server" CssClass="chzn-select ddlPage margin5px" TabIndex="12" Style="width: 280px; float: left">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-12 pading5px">

                                                <div class="form-group">
                                                    <asp:Label ID="lblCollection" runat="server" CssClass=" smLbl_to">Collection Team</asp:Label>
                                                    <asp:LinkButton ID="ibtnFindCollectionteam" CssClass="btn btn-primary srearchBtn" Visible="false" runat="server" OnClick="ibtnFindCollectionteam_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    <asp:DropDownList ID="ddlCollectionTeam" runat="server" CssClass="chzn-select ddlPage margin5px" TabIndex="12" Style="width: 280px;" OnSelectedIndexChanged="ddlCollectionTeam_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>


                                            <div class="col-md-12 pading5px">
                                                <div class="form-group">
                                                    <asp:Label ID="Label13" runat="server" CssClass="smLbl_to" Style="width: 100px;">Booking Date</asp:Label>
                                                    <asp:TextBox ID="txtBookDate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtBookDate"></cc1:CalendarExtender>
                                                    <asp:Label ID="Label2" runat="server" CssClass="smLbl_to">Agreement Date</asp:Label>
                                                    <asp:TextBox ID="txtAggrementdate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtAggrementdate_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtAggrementdate"></cc1:CalendarExtender>




                                                </div>

                                                <div class="clearfix"></div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="form-group">

                                                    <asp:Label ID="Label3" runat="server" CssClass="smLbl_to">Handover Date</asp:Label>
                                                    <asp:TextBox ID="txthandoverdate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txthandoverdate_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txthandoverdate"></cc1:CalendarExtender>
                                                    <asp:LinkButton ID="lbtnUpdateCAST" runat="server" CssClass="btn  btn-danger primaryBtn" OnClick="lbtnUpdateCAST_Click">Update</asp:LinkButton>

                                                </div>
                                            </div>

                                        </asp:Panel>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>

                            </div>

                            <hr />



                            <div class="row">

                                <div class="col-md-7">
                                    <div class=" table-responsive">
                                        <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnRowDeleting="gvPayment_RowDeleting" OnRowDataBound="gvPayment_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                            ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCode3" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                            Width="49px" ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                                <asp:TemplateField HeaderText="Description of Item">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lgcResDesc2" runat="server" BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                            Width="330px" ForeColor="Black"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lUpdatpayment" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatpayment_Click">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date ">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvDate" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Height="20px"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px" Font-Size="11px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtgvDate"></cc1:CalendarExtender>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lTotalPayment" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" OnClick="lTotalPayment_Click">Total Payment</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Percent" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvtPercent" runat="server" BackColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                            Width="40px" ForeColor="Black"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="+" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnJobAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent" OnClick="lbtnJobAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>


                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />

                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Job" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvjobdesc" runat="server" BackColor="Transparent" BorderStyle="None" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobdesc"))%>'
                                                            Width="200px" ForeColor="Black"></asp:Label>


                                                    </ItemTemplate>


                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="LO Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvLOAmt" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schloamt")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="90px" Font-Size="11px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lfLOAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Company Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="90px" Font-Size="11px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="jobcode" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvjobcode" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobcode")) %>'
                                                            Width="49px" ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvrmrks" runat="server" BackColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                            Width="120px" ForeColor="Black">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </div>



                                </div>


                                <div class="col-md-5">

                                    <asp:Panel ID="Panel3" runat="server">

                                        <div class="form-group">

                                            <div class="col-md-12 pading5px">

                                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">First Ins. Date</asp:Label>
                                                <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                                <asp:Label ID="Label5" runat="server" Font-Size="11px" CssClass="lblTxt lblName">Total Installement</asp:Label>
                                                <asp:TextBox ID="txtTInstall" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>


                                            </div>
                                            <div class="col-md-12 pading5px">
                                                <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Duration</asp:Label>
                                                <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="True"
                                                    CssClass="ddlPage" Width="120px">
                                                    <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                    <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                    <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                                    <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                    <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                                    <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                    <asp:ListItem Value="7">7  Month</asp:ListItem>
                                                    <asp:ListItem Value="8">8  Month</asp:ListItem>
                                                    <asp:ListItem Value="9">9  Month</asp:ListItem>
                                                    <asp:ListItem Value="10">10  Month</asp:ListItem>
                                                    <asp:ListItem Value="11">11  Month</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:LinkButton ID="lbtnGenerate" runat="server" OnClick="lbtnGenerate_Click" CssClass="btn btn-primary primaryBtn">Generate</asp:LinkButton>

                                                <%-- <asp:LinkButton ID="lbnGenerate2" runat="server" OnClick="lbnGenerate2_Click" CssClass="btn btn-primary primaryBtn">Generate</asp:LinkButton>--%>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>


                                    </asp:Panel>

                                    <asp:Panel ID="PanelAddIns" runat="server" Visible="False">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Installement</asp:Label>
                                                <asp:TextBox ID="txtsrchInstallment" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ibtnFindInstallment" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindInstallment_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-6 pading5px asitCol3">
                                                <asp:DropDownList ID="ddlInstallment" runat="server" CssClass="form-control inputTxt" TabIndex="12">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-2">
                                                <asp:LinkButton ID="lbtnAddInstallment" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnAddInstallment_Click">Add</asp:LinkButton>
                                            </div>
                                            <div class="clearfix"></div>

                                        </div>
                                    </asp:Panel>


                                    <asp:Panel ID="pnlSlab" runat="server" Visible="False">
                                        <div class="form-group">
                                            <div class="col-md-12 pading5px">
                                                <asp:Label ID="lblfrmslab" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                                <asp:TextBox ID="txtfrmslab" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                <asp:Label ID="lbltoslab" runat="server" CssClass="lblTxt lblName">To</asp:Label>
                                                <asp:TextBox ID="txttoslab" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                <asp:Label ID="lblinsamt" runat="server" CssClass="lblTxt lblName">Installment </asp:Label>
                                                <asp:TextBox ID="txtperslabamt" runat="server" CssClass="inputTxt inpPixedWidth" Style="text-align: right;"></asp:TextBox>
                                                <asp:LinkButton ID="lbtnSlab" runat="server" CssClass="btn btn-primary primaryBtn margin5px" OnClick="lbtnSlab_Click">Put Data</asp:LinkButton>
                                            </div>


                                            <div class="clearfix"></div>

                                        </div>
                                    </asp:Panel>

                                    <hr />

                                    <div class="form-group" style="margin-top: 66px;">
                                        <asp:Panel ID="Panel5" runat="server">
                                            <div class="form-group">
                                                <asp:Label ID="lPays" runat="server" CssClass="lblTxt lblName">Payment Shedule</asp:Label>
                                                <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                                <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True"
                                                    CssClass="chkBoxControl"
                                                    OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment" />
                                                <asp:CheckBox ID="chkSegment" runat="server" AutoPostBack="True"
                                                    CssClass="chkBoxControl"
                                                    OnCheckedChanged="chkSegment_CheckedChanged" Text="Slab" />

                                                <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                                                    CssClass="chkBoxControl"
                                                    OnCheckedChanged="chkAddIns_CheckedChanged" Text="Add.Installment" />
                                            </div>


                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>











                            <asp:Panel ID="Panel4" runat="server" Visible="false">

                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="lblAddWork" runat="server" CssClass="lblTxt lblName" Text="Additional Work"></asp:Label>


                                                <asp:Label ID="lblValAddWork" runat="server" CssClass="lblTxt lblName txtAlgLeft">

                                                </asp:Label>
                                                <asp:Label ID="lmsg111" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>

                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="lblDedWork" runat="server" CssClass="lblTxt lblName" Text="Deduction Work"></asp:Label>


                                                <asp:Label ID="lblValDedWork" runat="server" CssClass="lblTxt lblName txtAlgLeft">

                                                </asp:Label>

                                            </div>
                                            <div class="clearfix"></div>

                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="lblNetTotalPayment" Font-Size="11px" runat="server" CssClass="lblTxt lblName" Text="Net Total Payment"></asp:Label>


                                                <asp:Label ID="lblValNetTotalPayment" runat="server" CssClass="lblTxt lblName txtAlgLeft">

                                                </asp:Label>

                                            </div>
                                            <div class="clearfix"></div>

                                        </div>
                                    </div>
                                </fieldset>


                            </asp:Panel>

                        </asp:View>


                        <asp:View ID="VLoanInfo" runat="server">

                            <div class="row">
                                <asp:LinkButton ID="lbtnBackCost" CssClass="btn btn-danger primaryBtn" runat="server" OnClick="lbtnBack_Click">Back</asp:LinkButton>
                                <asp:GridView ID="gvLoanInformation" CssClass=" table-striped table-hover table-bordered grvContentarea" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="831px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" ForeColor="Black"
                                                    Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodeLoan" runat="server" ForeColor="Black" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc3" runat="server" Font-Size="11px" ForeColor="Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgvalloan" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdateLoanInfo" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" OnClick="lUpdateLoanInfo_Click"
                                                    Style="text-decaration: none;">Update Loan Info</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvValloan" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Height="20px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="510px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>



                        <asp:View ID="ViewResStatus" runat="server">
                            <asp:LinkButton ID="lbtnBackResStatus" runat="server" OnClick="lbtnBack_Click" CssClass="btn btn-danger primaryBtn">Back</asp:LinkButton>
                            <div class="row">
                                <asp:GridView ID="gvRegStatus" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="831px"
                                    Style="margin-right: 0px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" ForeColor="Black"
                                                    Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodeReg" runat="server" ForeColor="Black"
                                                    Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDescReg" runat="server" Font-Size="11px" ForeColor="Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgvalReg" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdateRegis" CssClass="btn btn-danger primaryBtn" runat="server" OnClick="lUpdateRegis_Click">Update Registration</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <table style="width: 13%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblLgDept" runat="server"
                                                                Style="text-align: center" Text="Received from Legal" Width="106px"
                                                                Height="16px"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblLgDept0" runat="server" Height="16px"
                                                                Style="text-align: center" Text="Status &amp; Date" Width="106px"></asp:Label>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvValRecleg" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Height="20px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="200px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="">

                                            <HeaderTemplate>
                                                <table style="width: 13%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblLgDept" runat="server"
                                                                Style="text-align: center" Text="Provided to Client" Width="106px"
                                                                Height="16px"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblLgDept0" runat="server" Height="16px"
                                                                Style="text-align: center" Text="Status &amp; Date" Width="106px"></asp:Label>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvValprotoclient" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Height="20px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc2")) %>'
                                                    Width="200px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                    </asp:MultiView>

                </div>
            </div>


            <div id="AddJob" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content  ">
                        <div class="modal-header">

                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Add New Job  </h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row-fluid">
                                <asp:Label ID="lblsircode" runat="server" Visible="false"></asp:Label>
                                <div class="form-group">
                                    <label id="lblddlproject" runat="server" class="col-md-2">Job</label>
                                    <div class="col-md-10">
                                        <asp:ListBox ID="lstJob" runat="server" SelectionMode="Multiple" Style="height: 50px !important;"
                                            data-placeholder="Choose Job......" multiple="true" class="form-control chosen-select"></asp:ListBox>
                                    </div>
                                </div>


                            </div>


                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddJob" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddJob();" OnClick="lbtnAddJob_Click"><span class="glyphicon glyphicon-plus"></span> Add </asp:LinkButton>


                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



