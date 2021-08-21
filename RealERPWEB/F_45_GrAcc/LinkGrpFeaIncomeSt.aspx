<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpFeaIncomeSt.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpFeaIncomeSt" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>
                                        <asp:Label ID="lblActDesc" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" Width="380px"></asp:Label>



                                    </div>
                                  
                                </div>

                               

                               
                            </div>
                        </fieldset>
                    </div>

          
                           
                                <asp:GridView ID="gvFeaIncomeSt" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True"  CssClass="table-striped table-hover table-bordered grvContentarea"
                                    onrowdatabound="gvFeaIncomeSt_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInfoCode" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Info Desc" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInfodesc" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>




                                      
                                         <asp:TemplateField HeaderText="Items Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgroupdesc" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" Font-size="11px" 
                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                                          
                                                          
                                                          Width="250px"></asp:Label>                                      
                                                  
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Orginal Value">
                                          
                                            <ItemTemplate>
                                              <asp:HyperLink ID="HLgOamt"   runat="server" Target="_blank"  BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px"  style="font-size:12px; color:Black; text-decoration:none; text-align:right;"  
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orgamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revised Value">
                                          
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvrevisedvalue" runat="server" Target="_blank"  BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="font-size:12px; color:Black; text-decoration:none; text-align:right;" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="% Orginal">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOpar" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orparcent")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="% Revised">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRpar" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rparcent")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="70px"></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

