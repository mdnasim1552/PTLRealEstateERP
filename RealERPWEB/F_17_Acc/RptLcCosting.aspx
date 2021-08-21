
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptLcCosting.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptLcCosting" %>
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
                            <div class="col-md-12">

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

                                           <asp:Label ID="Label1" runat="server" CssClass="smLbl_to" Text="To:"> </asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>


                            </div>
                        </div>

                    </div>

                </fieldset>
                <div class="table table-responsive">
                    <asp:GridView ID="gvlcCosting" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvlcCosting_PageIndexChanging"
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
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                <HeaderStyle HorizontalAlign="Left"  Font-Names="Century Gothic"/>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Resource Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvresdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                                <HeaderStyle HorizontalAlign="Left"  Font-Names="Century Gothic"/>
                            </asp:TemplateField>


                                   
                                                              

                             <asp:TemplateField HeaderText="OP Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvopamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>


                                <FooterTemplate>
                                    <asp:Label ID="lgvFopamt" runat="server" Font-Bold="True" Font-Size="12px"
                                           ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                               </FooterTemplate>

                             <HeaderStyle HorizontalAlign="Center"  Font-Names="Century Gothic"/>
                             <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                              <FooterStyle HorizontalAlign="Right"  Font-Names="Century Gothic"/>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dr. Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvdramt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFdramt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Names="Century Gothic"/>
                                <FooterStyle HorizontalAlign="Right"  Font-Names="Century Gothic"/>
                              <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                            
                             <asp:TemplateField HeaderText="Cr. Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFcramt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" />
                                <FooterStyle HorizontalAlign="Right"  Font-Names="Century Gothic"/>
                                <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Closing Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvclsamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFclsamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Century Gothic" />
                                <FooterStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
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


