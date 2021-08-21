<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPrjWiseMaterialCosting.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptPrjWiseMaterialCosting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            var gvprjcosting = $('#<%=this.gvprjcosting.ClientID %>');
            gvprjcosting.Scrollable();


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

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

              
                     
                   
                <div class="table-responsive">
                            <asp:GridView ID="gvprjcosting" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                                OnPageIndexChanging="gvprjcosting_PageIndexChanging">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>

                                 <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name" Width="100px"></asp:Label>
                                     <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                      CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span>
                                 
                                    </asp:HyperLink>
                                </HeaderTemplate>

                                <FooterTemplate>
                                   
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblgvProjDesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="150px" Font-Size="11px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left"/>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                                    
                                    
                                    
                                    <asp:TemplateField HeaderText="Description of Materials">
                                         <FooterTemplate>
                                                <asp:Label ID="lblFgvmatrese" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right">Total :</asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    


                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                                <asp:Label ID="lblgvFApQty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordrqty" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                   Width="70px"></asp:Label>
                                        </ItemTemplate>

                                         <FooterTemplate>
                                                <asp:Label ID="lblgvFordrqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvamount" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                         <FooterTemplate>
                                                <asp:Label ID="lblFgvamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" />
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
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





