
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BillApprovalSheet.aspx.cs" Inherits="RealERPWEB.F_17_Acc.BillApprovalSheet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

            var gv = $('#<%=this.gvbillApp.ClientID %>');
            gv.Scrollable();
        }
        
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


                                       <asp:Label ID="Label5" runat="server" CssClass=" smLbl_to">From</asp:Label>
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label6" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inpPixedWidth"
                                                TabIndex="1"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>


                                        <asp:Label ID="Label9" runat="server" CssClass="smLbl_to" Text="Bundle Date."></asp:Label>

                                        <asp:TextBox ID="txtBundleDat" runat="server" CssClass="inputTxt inputName inpPixedWidth"
                                                TabIndex="1"></asp:TextBox>
                                       <cc1:CalendarExtender ID="txtBundleDat_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtBundleDat" TodaysDateFormat=""></cc1:CalendarExtender>


                                     <%--   <div class="col-md-3">
                                                <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">User Name :</asp:Label>            

                                           
                                                <asp:DropDownList ID="ddlUserList" runat="server" CssClass="chzn-select form-control  inputTx" AutoPostBack="True" Width="150px">
                                                </asp:DropDownList>

                                            </div>--%>
                                       <%-- <div class="colMdbtn">--%>
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick" TabIndex="11">Ok</asp:LinkButton>

                                  <%--      </div>--%>

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
                        <asp:GridView ID="gvbillApp" runat="server"
                            AutoGenerateColumns="False" ShowFooter="True" Width="482px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="pactcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpactcodeno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Bill No" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvorBillno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
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
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True"
                                           OnCheckedChanged="chkAll_CheckedChanged" Text="ALL " />
                                    </HeaderTemplate>

                                </asp:TemplateField>

                                

                                <asp:TemplateField HeaderText="Entry Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBilldate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudate")).ToString("dd-MMM-yyyy") %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

           
                                    <asp:TemplateField HeaderText="Voucher No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBillvou1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>                        
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Account Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProjDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnSelectedBill" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnSelectedBill_Click">Selected Bill</asp:LinkButton>
                                    </FooterTemplate>
                                   
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdeptname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Particulars">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "narrations")) %>'
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

                            <asp:GridView ID="gvbundle" runat="server" AllowPaging="false"
                                AutoGenerateColumns="False" ShowFooter="True" Width="599px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                OnRowDeleting="gvbundle_OnRowDeleting" OnPageIndexChanging="gvbundle_PageIndexChanging">
                                <PagerSettings Position="TopAndBottom" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRSlNo1" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    
                                    
                                    <asp:TemplateField HeaderText="Bill No" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="gvappBillno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>                            
                                    <HeaderStyle HorizontalAlign="Left" />                                                            
                                     </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Entry Date">
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" runat="server"
                                                Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                     
                                    
                                    <asp:TemplateField HeaderText="Voucher No">
                                    <ItemTemplate>
                                        <asp:Label ID="gvappBillno1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>                        
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Account Head">
                                        <ItemTemplate>
                                            <asp:Label ID="pactdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>                                                                                                     
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeptname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>                                                                                                     
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Particulars">
                                        <ItemTemplate>
                                            <asp:Label ID="txtisurmk" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "narrations")) %>'
                                                Width="300px" BackColor="Transparent"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                           
                                      <asp:LinkButton ID="lnkupdateBill" runat="server" CssClass="btn btn-danger primaryBtn"
                                               OnClick="lnkupdate_OnClick">Update</asp:LinkButton>                                                                             
                                         </FooterTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Left"/>
                                           <FooterStyle HorizontalAlign="Right" Font-Bold="True"/>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvappbill" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFgvappbill" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
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

