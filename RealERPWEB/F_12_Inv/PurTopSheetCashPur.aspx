<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurTopSheetCashPur.aspx.cs" Inherits="RealERPWEB.F_12_Inv.PurTopSheetCashPur" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName" Text="Serial No:"></asp:Label>
                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="inputTxt inputBox50px"></asp:Label>


                                        <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>


                                        <asp:Label ID="Label9" runat="server" CssClass="smLbl_to" Text="Reference No."></asp:Label>

                                        <asp:TextBox ID="txtMIsuRef" runat="server" CssClass="inputTxt inputDateBox" TabIndex="3"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick" TabIndex="11">Ok</asp:LinkButton>

                                        </div>

                                        <div class="col-md-3 pading5px pull-right">
                                            <asp:LinkButton ID="lbtnPrevISSList" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lbtnPrevISSList_Click">Prev. List:</asp:LinkButton>
                                            <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass=" ddlPage inputTxt" Width="130px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                                        </div>

                                    </div>
                                </div>

                          <asp:Panel ID="pnlCashpurchase" runat="server" Visible="False">
                         <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:Label ID="lblbillno" runat="server" CssClass="lblTxt lblName" Text="Req No:"></asp:Label>
                                        <asp:DropDownList ID="ddlReqno" runat="server" CssClass=" ddlPage inputTxt" Width="130px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 asitCol2 pading5px">
                                        <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lnkAddNew_OnClick" TabIndex="11">Add New</asp:LinkButton>
                                    </div>
                                </div> 
                                
                            </div>
                        </fieldset>
                    </asp:Panel>
                                <%--   <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:LinkButton ID="lbtnPrevISSList" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lbtnPrevISSList_Click">Prev. Issue. List:</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass=" ddlPage inputTxt" Width="482px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                                    </div>
                                </div>--%>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="pnlbill" runat="server" Visible="false">
                            <asp:GridView ID="gvbill" runat="server"
                                AutoGenerateColumns="False" ShowFooter="True" Width="482px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Req No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvorBillno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkitem" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="1" %>'
                                                Width="20px" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkAllfrm_OnCheckedChanged" Text="ALL " />
                                        </HeaderTemplate>

                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSuplDesc" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ssirdesc").ToString() %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnSelectedOrdr" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnSelectedOrdr_OnClick">Selected Requisition</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Entry Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreqdate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Requistion No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBillno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))  %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="MRF No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmrfno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))  %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Req. Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgFBillamt" runat="server" Width="80px" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbillamt" runat="server" Width="80px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0.000;-#,##0.000; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />


                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Carring">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgFCaramt" runat="server" Width="80px" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCaramt" runat="server" Width="80px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "carring")).ToString("#,##0.000;-#,##0.000; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />


                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgFtoamt" runat="server" Width="80px" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTotal" runat="server" Width="80px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0.000;-#,##0.000; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />


                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>



                        </asp:Panel>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ViewBundle" runat="server">
                            <div class="row">
                                <asp:GridView ID="gvbundle" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="599px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDeleting="gvbundle_OnRowDeleting" OnPageIndexChanging="gvbundle_PageIndexChanging">
                                    <PagerSettings Position="TopAndBottom" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                       
                                        
                                         <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDesccp" runat="server" TextMode="MultiLine"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                                    OnClick="lnkupdate_OnClick">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Entry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqdatcp" runat="server"
                                                    Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req No" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblorgvreqnocp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1cp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrfnocp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))  %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpactdesccp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvparticualrcp" runat="server" TextMode="MultiLine"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="250px" BackColor="Transparent"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="total" runat="server" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Req. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtcp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvfreqamtcp" runat="server"  Style="text-align: right; font-weight:bold;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText=" Carring">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgFCaramtcp" runat="server" Width="80px" Style="text-align: right; font-weight:bold;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCaramtcp" runat="server" Width="80px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "carring")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />


                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgFtoamtcp" runat="server" Width="80px" Style="text-align: right; font-weight:bold;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotalcp" runat="server" Width="80px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />


                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>

                        </asp:View>


                    </asp:MultiView>





                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

