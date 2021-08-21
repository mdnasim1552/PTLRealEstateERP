<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurBillBundle.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurBillBundle" %>

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
        //hjk
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
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName" Text="Bundle No:"></asp:Label>
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


                                <%--   <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:LinkButton ID="lbtnPrevISSList" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lbtnPrevISSList_Click">Prev. Issue. List:</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass=" ddlPage inputTxt" Width="482px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                                    </div>
                                </div>--%>
                            </div>
                        </fieldset>
                    </div>
                    <asp:Panel ID="pnlBundleEntry" runat="server" Visible="False">
                         <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:Label ID="lblbillno" runat="server" CssClass="lblTxt lblName" Text="Bill No:"></asp:Label>
                                        <asp:DropDownList ID="ddlbillno" runat="server" CssClass=" ddlPage inputTxt" Width="130px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 asitCol2 pading5px">
                                        <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lnkAddNew_OnClick" TabIndex="11">Add New</asp:LinkButton>
                                    </div>
                                </div> 
                                
                            </div>
                        </fieldset>
                    </asp:Panel>

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
                                
                                  <asp:TemplateField HeaderText="Bill No" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvorBillno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
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
                                        <asp:LinkButton ID="lbtnSelectedOrdr" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnSelectedOrdr_OnClick">Selected Order</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Entry Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBilldate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBillno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1"))  %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Order No Or" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblorgvorderno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                            Width="70px">

                                        </asp:Label>
                                    </ItemTemplate>
                                   
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill Ref">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbillref" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Orderno No/Bill ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvorderno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProjDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Particulars">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgFBillamt" runat="server" Width="80px" style="text-align:right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbillamt" runat="server"  Width="80px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;-#,##0; ") %>' ></asp:Label>
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

                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ViewBundle" runat="server">

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
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Desc" runat="server" 
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
                                            <asp:Label ID="Label14" runat="server"
                                                Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Bill No" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblorgvBillno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                              
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvspcfcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order No" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txtorderorg" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Bill Ref">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbillrefno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order No/Bill ID">
                                        <ItemTemplate>
                                            <asp:Label ID="txtorder" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="pactdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Particulars">
                                        <ItemTemplate>
                                            <asp:Label ID="txtisurmk" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="300px" BackColor="Transparent"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                             <asp:Label ID="total" runat="server" Text="Total" Font-Bold="True"></asp:Label>
                                         </FooterTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Left"/>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvfamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvfamnt" runat="server"
                                                ></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="True"/>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            </asp:GridView>

                        </asp:View>


                    </asp:MultiView>





                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

