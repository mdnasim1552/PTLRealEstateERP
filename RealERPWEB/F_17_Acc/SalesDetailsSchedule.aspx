<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SalesDetailsSchedule.aspx.cs" Inherits="RealERPWEB.F_17_Acc.SalesDetailsSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <asp:Label ID="lbljavascript" runat="server"></asp:Label>

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

                            <div class="form-group">
                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="lbldateRange" runat="server" CssClass="lblTxt lblName" Text="Form"></asp:Label>
                                    <asp:TextBox ID="txtFromdat" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtFromdat_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFromdat">
                                    </cc1:CalendarExtender>
                                    <div class="smLbl_to">

                                        <asp:Label ID="lblTo" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtTodat" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtTodat_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtTodat">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                    </div>

                                </div>



                                <div class="col-md-3 pading5px pull-right">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#FF3300" Text="Please Wait......" Width="157px"
                                                Style="color: #FFFF99"></asp:Label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblAcchead" runat="server" CssClass="lblTxt lblName" Text="Accounts Head"></asp:Label>
                                    <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="imgsearch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgsearch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="ddlAccHeads" runat="server" CssClass="form-control inputTxt">
                                    </asp:DropDownList>

                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblreshead" runat="server" CssClass="lblTxt lblName" Text="Resource Head"></asp:Label>
                                    <asp:TextBox ID="txtSrcRes" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="imgsrcres" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgsrcres_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="ddlResHead" runat="server" CssClass="form-control inputTxt">
                                    </asp:DropDownList>

                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblreportlevel" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                    <asp:DropDownList ID="ddlRptlbl" runat="server" CssClass=" ddlPage inputTxt">
                                        
                                    <asp:ListItem>Level1</asp:ListItem>
                                                    <asp:ListItem>Level2</asp:ListItem>
                                                    <asp:ListItem>Level3</asp:ListItem>
                                                    <asp:ListItem Selected="True">Level4</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-2 pading5px asitCol3">
                                    <asp:CheckBox ID="chkWiZeroBal" runat="server" CssClass="checkBox" Text="Without Zero" />
                                </div>
                                 <div class="col-md-6 pading5px">
                                    <asp:Label ID="lbldateRange1" runat="server" CssClass=" smLbl_to">Message</asp:Label>
                               
                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                      
                        </fieldset>
                    </div>

                    <div class="table table-responsive">
                        <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="911px">
                            <Columns>
                                <asp:TemplateField HeaderText="Description of Account">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "actdesc4").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Dr. &lt;br&gt; Cr." FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Unit Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdescryption" runat="server" CssClass="GridLebelL"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "custname").ToString()+"<br />"+DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Amt." ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfopnamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopenamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing Amt." ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClosingamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            
<FooterStyle CssClass="grvFooter"/>
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


