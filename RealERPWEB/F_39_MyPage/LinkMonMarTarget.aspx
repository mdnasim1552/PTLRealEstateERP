<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkMonMarTarget.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.LinkMonMarTarget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">




    <style type="text/css">
        .style5 {
            width: 333px;
        }

        .style6 {
            width: 90px;
        }

        .style7 {
            width: 8px;
        }
    </style>

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

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
                                    <div class="col-md-5 pading5px ">
                                        <asp:Label ID="lblprono" runat="server" CssClass="lblTxt lblName">Program No</asp:Label>
                                        <asp:Label ID="lblvalpgno" runat="server" CssClass=" inputlblVal" Width="100px"></asp:Label>
                                        <asp:Label ID="lblref" runat="server" CssClass=" smLbl_to">Ref</asp:Label>
                                        <asp:Label ID="lblvalref" runat="server" CssClass=" inputlblVal" Width="120px"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <asp:GridView ID="gvPrjInfo" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="668px" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvPrjInfo_RowDataBound" >
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ActCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="200px" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Deptcode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdeptcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcod")) %>'
                                            Width="200px" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Activities">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtxtTotal" runat="server" Text="Total Days"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActivi" runat="server"
                                              Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'  

                                            Width="320px" Font-Size="11px">





                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                               
                                
                             
                                <asp:TemplateField HeaderText=" Target<br/>Start Date">

                                    <ItemTemplate>
                                        <asp:Label ID="lblStDate" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tstdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tstdat")).ToString("dd-MMM-yyyy")%>'
                                            Width="65px" Font-Size="11px"></asp:Label>
                                       
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Target<br/>End Date">

                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tenddat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tenddat")).ToString("dd-MMM-yyyy")%>'
                                            Width="65px" Font-Size="11px"></asp:Label>
                                     
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Target<br/>Duration">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFTotal" runat="server"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvduration" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;-#,##0; ")%>'
                                            Width="50px" Font-Size="11px" style="text-align:right"></asp:Label>
                                    </ItemTemplate>
                                   

                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                             



                                <asp:TemplateField HeaderText="Actual<br/>Start Date">

                                    <ItemTemplate>
                                         <asp:Label ID="lblacDate" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acstdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acstdat")).ToString("dd-MMM-yyyy")%>'
                                            Width="65px" Font-Size="11px"></asp:Label>
                                      
                                     
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Actual<br/>End Date">

                                    <ItemTemplate>
                                         <asp:Label ID="lblacendDate" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acenddat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acenddat")).ToString("dd-MMM-yyyy")%>'
                                            Width="65px" Font-Size="11px"></asp:Label>
                                      
                                     
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Actual<br/>Duration">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFacTotal" runat="server"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvacduration" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aduration")).ToString("#,##0;-#,##0; ")%>'
                                            Width="50px" Font-Size="11px" style="text-align:right"></asp:Label>
                                    </ItemTemplate>
                                   

                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Delay">

                                   

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdelay" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "deloadv")).ToString("#,##0;-#,##0; ")%>'
                                            Width="50px" Font-Size="11px" style="text-align:right"></asp:Label>
                                    </ItemTemplate>
                                   

                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                                      
                                  <asp:TemplateField HeaderText="Responsibility">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                 <asp:TemplateField HeaderText="Comments">
                                       
                                      
                                        <ItemTemplate>



                                            <asp:HyperLink ID="hlnkgvcomments" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: left; background-color: Transparent; color: blue;"
                                                Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comments"))  %>'
                                                Width="80px">                                             
                                            
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>        


                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                        


                    </div>
                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





