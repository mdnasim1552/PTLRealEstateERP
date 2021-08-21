<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkLandDevTopSheet.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LinkLandDevTopSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                          <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12 pading5px">

                                        <asp:Label ID="prname" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="lblProjectName" runat="server" CssClass="smLbl_to" TabIndex="1" Width="300px"></asp:TextBox>
                                      
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                        
                        
                      
                    </div>
                    <div class="table table-responsive">
                         <asp:GridView ID="gvFeaPrjRep" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="785px" OnRowDataBound="gvFeaPrjRep_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblinfocde" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Group Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgvgroupdesc" runat="server" AutoCompleteType="Disabled" ForeColor="#337ab7"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subgrpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                        
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "subgrpdesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "infdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "subgrpdesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")).Trim(): "")
                                                                    %>'
                                        Width="180px"></asp:Label>

                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Item" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvItemdescRep" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lgUnitnumRep" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <asp:Label ID="lgSizerep" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Number">
                                <ItemTemplate>
                                    <asp:Label ID="lgnumrep" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "number")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total SFT / Quantity">

                                <ItemTemplate>
                                    <asp:Label ID="lgtsizecRep" runat="server" Font-Size="11px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0;(#,##0); ") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate/SFT">

                                <ItemTemplate>
                                    <asp:Label ID="lgsalraterep" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmtrep" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmtc0" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="White" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Percentage %" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvper" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pecent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
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
            </div>

            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


