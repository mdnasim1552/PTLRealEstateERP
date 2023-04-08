<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPurchaseOrdItemRate.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPurchaseOrdItemRate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            var gvReqStatusAp = $('#<%=this.gvOrdItemStatus.ClientID %>');
            gvReqStatusAp.Scrollable();


            $('.chzn-select').chosen({ search_contains: true });


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
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Style="width: 336px" CssClass="chzn-select form-control  inputTxt">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkbtnOk" runat="server" Style="margin-left: -50px" CssClass="btn btn-primary okBtn" OnClick="lnkbtnOk_Click">ok</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Resource</asp:Label>
                                        <asp:TextBox ID="txtSrcResource" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>

                                        <asp:LinkButton ID="imgbtnFindResource" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindResource_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlResource" runat="server" Style="width: 336px" CssClass="chzn-select form-control  inputTxt">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:CheckBox ID="CheckReqApp" runat="server" CssClass="chkBoxControl margin5px" Text="Requisition Qty" Visible="false" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-3  pading5px">

                                    <asp:Label ID="Label13" runat="server" CssClass=" lblName lblTxt"
                                        Text="Date:"></asp:Label>


                                    <asp:TextBox ID="txtFDate" runat="server" CssClass="inputDateBox" TabIndex="5"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>

                                    <asp:Label ID="Label3" runat="server" CssClass="smLbl_to"
                                        Text="to:"></asp:Label>


                                    <asp:TextBox ID="txttodate" runat="server" CssClass="inputDateBox" TabIndex="5"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                </div>



                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" Text="Size:" CssClass="lblName lblTxt"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage"
                                            TabIndex="4">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>

                                    
                                </div>


                            </div>
                        </fieldset>
                    </div>

          <asp:GridView ID="gvOrdAvg" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="501px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjDescAvg" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Item Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvitemdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="MRF No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMrfNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                 
                                    <asp:TemplateField HeaderText="Description of Materials">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDescAvg" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="140px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                   
                             <%--       <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpfDescAvg" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                <asp:Label ID="lblgvFspcfavg" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"> Total :</asp:Label>
                                         </FooterTemplate>


  
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnitAvg" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                        

                                    
                                        



                                    <asp:TemplateField HeaderText="P.O. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQtyAvg" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                                <asp:Label ID="lblgvFQtyAvg" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Average Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordrateAvg" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                         <FooterTemplate>
                                                <asp:Label ID="gvFlblgvordrateAvg" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    
                                        <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordamtAvg" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                         <FooterTemplate>
                                                <asp:Label ID="gvFgvordamtAvg" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    



<%--                                    <asp:TemplateField HeaderText="Entry User">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvEntryUser" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eusrname")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField>--%>
                                    

                                    
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />


                            </asp:GridView>
                     
                            <asp:GridView ID="gvOrdItemStatus" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                                OnPageIndexChanging="gvOrdItemStatus_PageIndexChanging">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Item Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvitemdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="MRF No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMrfNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                 
                                    <asp:TemplateField HeaderText="Description of Materials">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="140px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpfDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                <asp:Label ID="lblgvFspcf" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"> </asp:Label>
                                         </FooterTemplate>


  
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvorderno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno2")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                <asp:Label ID="lblForderno" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>

                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    
                                        <asp:TemplateField HeaderText="Order Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvorderdat" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                <asp:Label ID="lblFgvorderdat" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right">Total :</asp:Label>
                                         </FooterTemplate>

                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsupnam" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="P.O. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQty" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                                <asp:Label ID="lblgvFQty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="P.O Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordrate" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                         <FooterTemplate>
                                                <asp:Label ID="gvFlblgvordrate" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    
                                        <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordamt" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                         <FooterTemplate>
                                                <asp:Label ID="gvFgvordamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    



<%--                                    <asp:TemplateField HeaderText="Entry User">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvEntryUser" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eusrname")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField>--%>
                                    

                                    
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />


                            </asp:GridView>
                          

                    
                    

                        
                        

                  
                            
                    
             
                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

