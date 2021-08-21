<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProjectWiseCollection.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptProjectWiseCollection" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript">

    $(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
    });

    function pageLoaded() {
        $('.chzn-select').chosen({ search_contains: true });
           <%-- var gv = $('#<%=this.gvSubBill.ClientID %>');
            gv.Scrollable();--%>
        }

</script>
    <div class="container moduleItemWrpper">
        <div class="contentPart">
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
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">

                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-3 asitCol3 pading5px">

                                <asp:Label ID="Label5" runat="server"
                                    CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"
                                    Font-Bold="True"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>
                            <div class="col-md-4 asitCol4 pading5px">
                                <asp:DropDownList ID="ddlProjectName" CssClass=" ddlPage chzn-select" runat="server" Font-Bold="True"
                                    Width="300px">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                    QueryPattern="Contains" TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>

                                
                            </div>
                            <div class="col-md-1 pading5px pull-left">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>



                        </div>
                        <div class="form-group">
                            <div class="col-md-4 asitCol4 pading5px">

                                <asp:Label ID="lblPage0" runat="server" CssClass="lblTxt lblName" Text="Size:" ></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                    CssClass="ddlPage"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="71px">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>


                                
                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to" Text="Date:"> </asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                            </div>
                        </div>

                    </div>

                </fieldset>
                <div class="table table-responsive">
                    <asp:GridView ID="gvprjstatus" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvprjstatus_PageIndexChanging"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Names="Century Gothic"/>
                                <ItemStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgactdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                <HeaderStyle HorizontalAlign="Left"  Font-Names="Century Gothic"/>
                            </asp:TemplateField>

                                   
                                    <asp:TemplateField HeaderText="Name Of Allotte" >

                                    <HeaderTemplate>
                                    <asp:Label ID="lgacuname" runat="server" Font-Bold="True" Text="Name Of Allotte" Width="180px"></asp:Label>

                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i>
                                    </asp:HyperLink>
                                </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgacuname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>

                            <%--  <asp:TemplateField HeaderText="Name Of Allotte">
                                <ItemTemplate>
                                    <asp:Label ID="lgacuname" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                           ForeColor="#000" Style="text-align: right" Width="200px"></asp:Label>
                               </FooterTemplate>

                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>--%>



                            <asp:TemplateField HeaderText="Apt. Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgudesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left"  Font-Names="Century Gothic"/>
                                 <ItemStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Sft Size">
                                <ItemTemplate>
                                    <asp:Label ID="lgvsftsize" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Names="Century Gothic"/>
                                 <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Contracted Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTVal" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalsval")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>


                                <FooterTemplate>
                                    <asp:Label ID="lgvFTval" runat="server" Font-Bold="True" Font-Size="12px"
                                           ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                               </FooterTemplate>

                             <HeaderStyle HorizontalAlign="Center"  Font-Names="Century Gothic"/>
                             <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                              <FooterStyle HorizontalAlign="Right"  Font-Names="Century Gothic"/>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Received </br> Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvrecamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFreceivedamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Names="Century Gothic"/>
                                <FooterStyle HorizontalAlign="Right"  Font-Names="Century Gothic"/>
                              <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                            
                             <asp:TemplateField HeaderText="Modification  </br> Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmodamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "modfiamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFlgvmodamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" />
                                <FooterStyle HorizontalAlign="Right"  Font-Names="Century Gothic"/>
                                <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Increase Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvIncreseamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "increseamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFIncreseamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" />
                                <FooterStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="Utility Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvutilityamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utilityamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFutilityamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" />
                                <FooterStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Delay Charge </br> Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvdelayamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delayamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFdelayamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Names="Century Gothic"/>
                                <FooterStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                               <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Association">
                                <ItemTemplate>
                                    <asp:Label ID="lgvassociationamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "associaamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFassociationamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" />
                                <FooterStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                               <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Total Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtotalamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalrecamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFtotalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" />
                                <FooterStyle HorizontalAlign="Right"  Font-Names="Century Gothic"/>
                               <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>




                              <asp:TemplateField HeaderText="Balance Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbalamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balance")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                   <asp:Label ID="lgvFbalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Names="Century Gothic"/>
                                <FooterStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                               <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>
                       
                       
                           

                            <asp:TemplateField HeaderText="Car Parking">
                                <ItemTemplate>
                                    <asp:Label ID="lgvparkam" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cparkam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                 <FooterTemplate>
                                   <asp:Label ID="lgvFparkam" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Names="Century Gothic" Width="70px" />
                                 <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                <FooterStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Position">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpersft" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "position")) %>'
                                        Width="35px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Names="Century Gothic" Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                              

                              <asp:TemplateField HeaderText="Utility">
                                <ItemTemplate>
                                    <asp:Label ID="lgvutility" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utlityam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                   <FooterTemplate>
                                   <asp:Label ID="lgvFutility" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" Width="70px" />
                                <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                  <FooterStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                        
                                  
                            </asp:TemplateField>
                           

                            <asp:TemplateField HeaderText="Loan">
                                <ItemTemplate>
                                    <asp:Label ID="lgvloan" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "loan")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" Width="70px" />
                                <ItemStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Hand Over Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpersft" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "handovrdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" Width="70px" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="Registration">
                                <ItemTemplate>
                                    <asp:Label ID="lgvregis" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "regdesc")) %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" Width="70px" />
                            
                                <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
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
    </div>

</asp:Content>
